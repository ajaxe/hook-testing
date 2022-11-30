using Microsoft.AspNetCore.Mvc;

namespace ApogeeDev.WebhookTester.Pages.Shared.Components.UserProfile;

public class UserProfileViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var currentUser = new ApogeeDev.WebhookTester.Common.ViewModels.UserProfile(UserClaimsPrincipal);
        return View(currentUser);
    }
}