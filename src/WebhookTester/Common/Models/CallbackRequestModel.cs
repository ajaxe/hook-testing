using Microsoft.Extensions.Primitives;

namespace ApogeeDev.WebhookTester.Common.Models;

public class CallbackRequestModel
{
    public Guid Id { get; set; }
    public Guid WebhookSessionId { get; set; }

    public Dictionary<string, List<string>> Headers { get; set; }

    public string RequestBody { get; set; }

    public DateTime ReceivedDate { get; set; }
    public IEnumerable<KeyValuePair<string, StringValues>> QueryString { get; internal set; }
    public string RequestMethod { get; internal set; }
    public IEnumerable<KeyValuePair<string, StringValues>> FormData { get; internal set; }
    public FormFileData[]? Files { get; internal set; }
}

public class FormFileData
{
    public string FileName { get; internal set; }
    public string Name { get; internal set; }
    public string ContentDisposition { get; internal set; }
    public string ContentType { get; internal set; }
    public long Length { get; internal set; }
}