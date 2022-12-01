using System.Security.Claims;
using ApogeeDev.WebhookTester.Common.Models;

namespace ApogeeDev.WebhookTester.Common.ViewModels;

public class UserProfile
{

    public ClaimsPrincipal User { get; }
    public UserProfile(ClaimsPrincipal user)
    {
        this.User = user;
    }
    private string _profileImageUrl;
    public string ProfileImageUrl => _profileImageUrl
        ?? (_profileImageUrl = GetClaimValue("picture"));
    private string _name;
    public string Name => _name ?? (_name = GetClaimValue("name"));

    private string _email;
    public string Email => _email ?? (_email = GetClaimValue(AppClaimTypes.Email));

    private string _identifier;
    public string Identifier => _identifier ?? (_identifier = GetClaimValue(AppClaimTypes.NameIdentifier));
    private string _idProvider;
    public string IdProvider => _idProvider ?? (_idProvider = GetClaimValue(AppClaimTypes.IdP));
    public string UserIdentifier => $"{IdProvider}:{Identifier}";

    private string GetClaimValue(string claimType)
    {
        return User.Claims.FirstOrDefault(c => c.Type == claimType)?.Value
            ?? string.Empty;
    }
}