namespace ApogeeDev.WebhookTester.Common.Models;

public class WebhookSession
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public string UserIdentifier { get; set; } = default!;
}