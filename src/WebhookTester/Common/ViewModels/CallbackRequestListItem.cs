namespace ApogeeDev.WebhookTester.Common.ViewModels;

public class CallbackRequestListItem
{
    public Guid Id { get; set; }
    public string RequestMethod { get; set; } = default!;
    public DateTime ReceivedDate { get; set; }
}