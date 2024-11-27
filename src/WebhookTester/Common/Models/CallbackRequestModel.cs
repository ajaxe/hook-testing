using Microsoft.Extensions.Primitives;

namespace ApogeeDev.WebhookTester.Common.Models;

public class CallbackRequestModel
{
    public Guid Id { get; set; }
    public Guid WebhookSessionId { get; set; }

    public Dictionary<string, List<string?>> Headers { get; set; } = default!;

    public string RequestBody { get; set; } = default!;

    public DateTime ReceivedDate { get; set; }
    public IEnumerable<KeyValuePair<string, StringValues>> QueryString { get; set; }
        = Enumerable.Empty<KeyValuePair<string, StringValues>>();
    public string RequestMethod { get; set; } = default!;
    public IEnumerable<KeyValuePair<string, StringValues>> FormData { get; set; }
        = Enumerable.Empty<KeyValuePair<string, StringValues>>();
    public FormFileData[]? Files { get; set; }
}

public class FormFileData
{
    public string FileName { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string ContentDisposition { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public long Length { get; set; }
}