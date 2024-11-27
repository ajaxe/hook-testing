using ApogeeDev.WebhookTester.Common.Models;
using Microsoft.Extensions.Primitives;

namespace ApogeeDev.WebhookTester.Common.Commands;

public class CallbackReceived
{
    public Guid WebhookSessionId { get; set; }
    public Dictionary<string, List<string?>> Headers { get; set; } = default!;
    public string RequestMethod { get; set; } = default!;
    public IEnumerable<KeyValuePair<string, StringValues>> QueryString { get; set; } = default!;

    public string RequestBody { get; set; } = default!;
    public IEnumerable<KeyValuePair<string, StringValues>> FormData { get; internal set; } = default!;
    public FormFileData[]? Files { get; internal set; }
}