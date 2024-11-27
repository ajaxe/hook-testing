using System.Security.Claims;
using ApogeeDev.WebhookTester.Abstractions;
using ApogeeDev.WebhookTester.AppService;
using ApogeeDev.WebhookTester.Common.Configuration;
using ApogeeDev.WebhookTester.Providers;
using ApogeeDev.WebhookTester.Workers;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace ApogeeDev.WebhookTester;

public static class StartupInitializers
{
    public static void ConfigureGoogleAuth(IServiceCollection services, OAuthOptions authOptions)
    {
        var authBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(ConfigureCookieOptions());

        ConfigureOpenIdConnect(services, authBuilder, authOptions);
    }

    private static void ConfigureOpenIdConnect(IServiceCollection services,
        AuthenticationBuilder authBuilder,
        OAuthOptions authOptions)
    {
        authBuilder.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
        {
            // configuring openIDconnect options
            options.NonceCookie.SecurePolicy = CookieSecurePolicy.Always;
            options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
            // How middleware persists the user identity? (Cookie)
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.GetClaimsFromUserInfoEndpoint = true;
            // How Browser redirects user to authentication provider?
            // (direct get)
            options.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;

            // How response should be sent back from authentication provider?
            //(form_post)
            options.ResponseMode = OpenIdConnectResponseMode.FormPost;

            // Who is the authentication provider? (IDP)
            options.Authority = authOptions.Authority;

            // Who are we? (client id)
            options.ClientId = authOptions.ClientId;

            // How does authentication provider know, we are ligit? (secret key)
            options.ClientSecret = authOptions.ClientSecret;

            // What do we intend to receive back?
            // (code to make for consequent requests)
            options.ResponseType = OpenIdConnectResponseType.Code;

            // Should there be extra layer of security?
            // (false: as we are using hybrid)
            options.UsePkce = true;

            // Where we would like to get the response after authentication?
            options.CallbackPath = authOptions.CallbackPath;
            options.SignedOutCallbackPath = authOptions.SignedOutCallbackPath;

            // Should we persist tokens?
            options.SaveTokens = true;
            options.Prompt = OpenIdConnectPrompt.SelectAccount;

            options.ClaimActions.MapUniqueJsonKey(ClaimTypes.Name, "name");
            options.ClaimActions.MapUniqueJsonKey(ClaimTypes.NameIdentifier, "sub");
            options.ClaimActions.MapUniqueJsonKey(ClaimTypes.Email, "email");
            options.ClaimActions.MapUniqueJsonKey("picture", "picture");
            options.ClaimActions.MapUniqueJsonKey(AppClaimTypes.IdP, "private:idp");

            // Should we request user profile details for user end point?
            options.GetClaimsFromUserInfoEndpoint = true;

            // What scopes do we need?
            options.Scope.Add("openid");
            options.Scope.Add("email");
            options.Scope.Add("phone");
            options.Scope.Add("profile");
        });
    }

    private static Action<CookieAuthenticationOptions> ConfigureCookieOptions()
    {
        return o =>
        {
            o.LoginPath = "/login";
            o.LogoutPath = "/logout";
            o.Cookie.Name = "oidc";
            o.Cookie.SameSite = SameSiteMode.None;
            o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            o.Cookie.IsEssential = true;
        };
    }

    public static void ConfigureInjectableServices(IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddTransient<IWebhookSessionService, WebhookSessionService>();
        services.AddTransient<ICallbackProcessor, CallbackProcessor>();
        services.AddTransient<IUserInfoProvider, UserInfoProvider>();

        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        services.AddHostedService<WebhookSessionCleanupWorker>();
    }
}