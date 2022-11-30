namespace ApogeeDev.WebhookTester.Common.Configuration;

public class AppOptions
{
    public const string SectionName = "AppOptions";
    public string GoogleClientId { get; set; }
    public string GoogleClientSecret { get; set; }
}