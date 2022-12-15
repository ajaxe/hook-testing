using ApogeeDev.WebhookTester.AppService;

namespace ApogeeDev.WebhookTester.Workers;

internal class WebhookSessionCleanupWorker : BackgroundService
{
    private readonly IWebhookSessionService webhookSessionService;
    private readonly ILogger<WebhookSessionCleanupWorker> logger;

    public WebhookSessionCleanupWorker(IWebhookSessionService webhookSessionService,
        ILogger<WebhookSessionCleanupWorker> logger)
    {
        this.webhookSessionService = webhookSessionService;
        this.logger = logger;
    }

    private TimeSpan CuttOff => TimeSpan.FromDays(2);
    private TimeSpan RunInterval => TimeSpan.FromDays(1.5);
    private TimeSpan DelayInterval => TimeSpan.FromHours(2);
    private DateTime? LastRunAt { get; set; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Attempting to cleanup webhook sessions");

            if (LastRunAt is null || (DateTime.UtcNow - LastRunAt) >= RunInterval)
            {
                var deletedCount = await webhookSessionService.CleanupWebhookSessions(CuttOff);
                logger.LogInformation("Deleted  webhook sessions: {DeletedCount}", deletedCount);
                LastRunAt = DateTime.UtcNow;
            }
            else
            {
                logger.LogInformation("Skipping cleanup {LastRunAt}", LastRunAt);
            }

            await Task.Delay(DelayInterval, stoppingToken);
        }

        if (stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Cancellation requested");
        }
    }
}