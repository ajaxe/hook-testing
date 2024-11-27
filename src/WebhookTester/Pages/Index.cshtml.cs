using ApogeeDev.WebhookTester.AppService;
using ApogeeDev.WebhookTester.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApogeeDev.WebhookTester.Pages;

public class IndexModel : PageModel
{
    private readonly IWebhookSessionService _sessionService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IWebhookSessionService sessionService, ILogger<IndexModel> logger)
    {
        _sessionService = sessionService;
        _logger = logger;
    }
    public IEnumerable<PerkDescription> Perks
    {
        get
        {
            yield return new PerkDescription
            {
                Title = "Anonymous use",
                Description = "No log in required to start using it."
            };
            yield return new PerkDescription
            {
                Title = "Free to use",
                Description = "Create as many anonymous webhook sessions needed, these remain active for 24hrs."
            };
            yield return new PerkDescription
            {
                Title = "Bookmark sessions",
                Description = "Up to 10 sessions can be saved for later, after log in."
            };
            yield return new PerkDescription
            {
                Title = "Inspect received callbacks",
                Description = "Simple view of received callback request"
            };
        }
    }

    public class PerkDescription
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
