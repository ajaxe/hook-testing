namespace ApogeeDev.WebhookTester.Common.Commands;

public class CallbackReceived
{
    public Guid WebhookSessionId { get; set; }
    public Dictionary<string, List<string>> Headers { get; set; }

    public string RequestBody { get; set; }
}