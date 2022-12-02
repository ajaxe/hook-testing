namespace ApogeeDev.WebhookTester.Common.ViewModels;

public class CallbackRequestListItem
{
    public Guid Id { get; set; }
    public string RequestMethod { get; set; }
    public DateTime ReceivedDate { get; set; }
}