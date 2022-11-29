using ApogeeDev.WebhookTester.Common.Models;

namespace ApogeeDev.WebhookTester.Common.ViewModels;

public class WebhookSessionView
{
    public WebhookSessionView()
    {
        CallRequests = new List<CallbackRequestListItem>();
        StartDate = DateTime.UtcNow;
    }

    public DateTime StartDate { get; set; }
    public Guid WebhookSessionId { get; set; }
    public string CallbackUrl => $"/callback/{WebhookSessionId}";
    public List<CallbackRequestListItem> CallRequests { get; set; }
    public CallbackRequestModel? MostRecentCallback { get; set; }

    public static WebhookSessionView Empty { get; } = new WebhookSessionView();
}