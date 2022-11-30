using ApogeeDev.WebhookTester.AppService;
using ApogeeDev.WebhookTester.Common.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ApogeeDev.WebhookTester;

public static class StartupInitializers
{
    public static void ConfigureGoogleAuth(IServiceCollection services, AppOptions appOptions)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(ConfigureCookieOptions());
    }

    private static Action<CookieAuthenticationOptions> ConfigureCookieOptions()
    {
        return options =>
        {
            var commonPath = new PathString("/Index"); ;
            options.AccessDeniedPath = commonPath;
            options.LoginPath = commonPath;
        };
    }

    public static void ConfigureInjectableServices(IServiceCollection services)
    {
        services.AddTransient<IWebhookSessionService, WebhookSessionService>();
        services.AddTransient<ICallbackProcessor, CallbackProcessor>();
    }
}