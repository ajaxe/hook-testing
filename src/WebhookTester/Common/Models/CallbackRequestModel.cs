using Microsoft.Extensions.Primitives;

namespace ApogeeDev.WebhookTester.Common.Models;

public class CallbackRequestModel
{
    public Guid Id { get; set; }
    public Guid WebhookSessionId { get; set; }

    public Dictionary<string, List<string>> Headers { get; set; }

    public string RequestBody { get; set; }

    public DateTime ReceivedDate { get; set; }
    public IEnumerable<KeyValuePair<string, StringValues>> QueryString { get; set; }
    public string RequestMethod { get; set; }
    public IEnumerable<KeyValuePair<string, StringValues>> FormData { get; set; }
    public FormFileData[]? Files { get; set; }
}

public class FormFileData
{
    public string FileName { get; set; }
    public string Name { get; set; }
    public string ContentDisposition { get; set; }
    public string ContentType { get; set; }
    public long Length { get; set; }
}