using ApogeeDev.WebhookTester.Common.Models;

namespace ApogeeDev.WebhookTester.Common.ViewModels;

public class WebhookSessionItemView
{
    public DateTime StartDate { get; set; }
    public Guid WebhookSessionId { get; set; }
    public string CallbackUrl => $"/callback/{WebhookSessionId}";
    public string SessionLink => $"/session/{WebhookSessionId}";
}