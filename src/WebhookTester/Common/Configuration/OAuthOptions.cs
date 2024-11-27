namespace ApogeeDev.WebhookTester.Common.Configuration;

public class OAuthOptions
{
    public const string SectionName = nameof(OAuthOptions);
    public string Authority { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
    public string CallbackPath { get; set; } = default!;
    public string SignedOutCallbackPath { get; set; } = default!;
}