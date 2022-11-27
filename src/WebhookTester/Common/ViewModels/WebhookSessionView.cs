using ApogeeDev.WebhookTester.Common.Models;

namespace ApogeeDev.WebhookTester.Common.ViewModels;

public class WebhookSessionView
{
    public WebhookSessionView()
    {
        CallRequests = new List<CallbackRequestModel>();
        StartDate = DateTime.UtcNow;
    }

    public DateTime StartDate { get; set; }
    public Guid WebhookSessionId { get; set; }
    public string CallbackUrl => $"/callback/{WebhookSessionId}";
    public List<CallbackRequestModel> CallRequests { get; set; }

    public static WebhookSessionView Empty { get; } = new WebhookSessionView();
}