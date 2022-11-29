using ApogeeDev.WebhookTester.AppService;
using ApogeeDev.WebhookTester.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApogeeDev.WebhookTester.Pages.Session;

public class IndexModel : PageModel
{
    private readonly IWebhookSessionService _sessionService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IWebhookSessionService sessionService, ILogger<IndexModel> logger)
    {
        _sessionService = sessionService;
        _logger = logger;
    }

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public WebhookSessionView NewSession { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (Id == default)
        {
            NewSession = await _sessionService.GetWebhookSession();
            return RedirectToPage("/Session/Index", new { id = NewSession.WebhookSessionId });
        }
        else
        {
            NewSession = await _sessionService.GetWebhookSession(Id);
        }

        return Page();
    }
}
