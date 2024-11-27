using System.Security.Claims;
using ApogeeDev.WebhookTester.Abstractions;
using ApogeeDev.WebhookTester.Common.ViewModels;

namespace ApogeeDev.WebhookTester.Providers;
public class UserInfoProvider : IUserInfoProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserInfoProvider(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }
    public UserProfile? GetUserProfile()
    {
        ClaimsPrincipal? currentUser = _httpContextAccessor.HttpContext?.User;
        if (currentUser is null)
        {
            return null;
        }
        return new UserProfile(currentUser);
    }
}