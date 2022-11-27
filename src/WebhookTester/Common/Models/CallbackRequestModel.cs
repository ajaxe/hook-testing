namespace ApogeeDev.WebhookTester.Common.Models;

public class CallbackRequestModel
{
    public Guid Id { get; set; }
    public Guid WebhookSessionId { get; set; }

    public Dictionary<string, List<string>> Headers { get; set; }

    public string RequestBody { get; set; }

    public DateTime ReceivedDate { get; set; }
}