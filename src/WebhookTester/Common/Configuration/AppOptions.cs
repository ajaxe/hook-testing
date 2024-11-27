namespace ApogeeDev.WebhookTester.Common.Configuration;

public class AppOptions
{
    public const string SectionName = "AppOptions";
    public string GoogleClientId { get; set; } = default!;
    public string GoogleClientSecret { get; set; } = default!;
    public int MaxSessionPerUser { get; set; } = 10;
}