using ApogeeDev.WebhookTester.Common.Models;
using Microsoft.Extensions.Primitives;

namespace ApogeeDev.WebhookTester.Common.Commands;

public class CallbackReceived
{
    public Guid WebhookSessionId { get; set; }
    public Dictionary<string, List<string>> Headers { get; set; }
    public string RequestMethod { get; set; }
    public IEnumerable<KeyValuePair<string, StringValues>> QueryString { get; set; }

    public string RequestBody { get; set; }
    public IEnumerable<KeyValuePair<string, StringValues>> FormData { get; internal set; }
    public FormFileData[]? Files { get; internal set; }
}