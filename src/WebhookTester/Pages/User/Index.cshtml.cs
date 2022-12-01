using ApogeeDev.WebhookTester.AppService;
using ApogeeDev.WebhookTester.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApogeeDev.WebhookTester.Pages.User;

public class IndexModel : PageModel
{
    private readonly IWebhookSessionService sessionService;

    public IndexModel(IWebhookSessionService sessionService)
    {
        this.sessionService = sessionService;
    }
    public List<WebhookSessionItemView> UserSessions { get; set; }
    public bool ShowUserSessions => UserSessions.Count > 0;
    public async Task<IActionResult> OnGetAsync()
    {
        UserSessions = await sessionService.GetCurrentUserSessions();
        return Page();
    }
}