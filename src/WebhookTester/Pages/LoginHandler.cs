using System.Security.Claims;
using ApogeeDev.WebhookTester.Common.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApogeeDev.WebhookTester.Pages;

[IgnoreAntiforgeryToken]
public class LoginHandler : PageModel
{
    public LoginHandler(ILogger<LoginHandler> logger)
    {
        this.logger = logger;
    }
    private const string CsrfCookieName = "g_csrf_token";
    private readonly ILogger<LoginHandler> logger;

    public async Task<IActionResult> OnPostGoogleSignInAsync(string credential,
        string g_csrf_token, [FromQuery] string currentPage)
    {
        IActionResult result = await AuthenticateUsingGoogleToken(credential, g_csrf_token, currentPage);
        return result;
    }
    public async Task<IActionResult> OnGetLogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToPage("Index");
    }

    private async Task<IActionResult> AuthenticateUsingGoogleToken(string credential,
        string g_csrf_token, string currentPage)
    {
        IActionResult? actionResult;

        if (!IsValidCsrfToken(g_csrf_token, out actionResult))
        {
            return actionResult ?? BadRequest();
        }

        try
        {
            var jwtToken = await GoogleJsonWebSignature.ValidateAsync(credential);

            await SignInAsync(jwtToken);

            actionResult = Redirect(currentPage);
        }
        catch (Google.Apis.Auth.InvalidJwtException ex)
        {
            logger.LogError(ex, "Error validating id_token. Message: " + ex.Message);

            actionResult = RedirectToPage("Index");
        }

        return actionResult;
    }

    private async Task SignInAsync(GoogleJsonWebSignature.Payload jwtToken)
    {
        var claims = new List<Claim>
            {
                new Claim(AppClaimTypes.Name, jwtToken.Name),
                new Claim(AppClaimTypes.NameIdentifier, jwtToken.Subject),
                new Claim(AppClaimTypes.Picture, jwtToken.Picture),
                new Claim(AppClaimTypes.Email, jwtToken.Email),
                new Claim(AppClaimTypes.IdP, "google"),
            };
        var claimsIdentity = new ClaimsIdentity(claims,
        CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
    }

    private bool IsValidCsrfToken(string csrfToken, out IActionResult? actionResult)
    {
        if (Request.Cookies[CsrfCookieName] != csrfToken)
        {
            logger.LogError("Invalid CSRF token");
            actionResult = RedirectToPage("Index");
            return false;
        }
        actionResult = null;
        return true;
    }

}