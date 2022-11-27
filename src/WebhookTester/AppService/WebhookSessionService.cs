using ApogeeDev.WebhookTester.Common.Models;
using ApogeeDev.WebhookTester.Common.ViewModels;
using Marten;

namespace ApogeeDev.WebhookTester.AppService;

public interface IWebhookSessionService
{
    Task<WebhookSessionView> GetWebhookSession(Guid webhookSessionId = default);
}

internal class WebhookSessionService : IWebhookSessionService
{
    private readonly IDocumentStore store;
    private readonly ILogger<WebhookSessionService> logger;

    public WebhookSessionService(IDocumentStore store, ILogger<WebhookSessionService> logger)
    {
        this.store = store;
        this.logger = logger;
    }

    public async Task<WebhookSessionView> GetWebhookSession(Guid webhookSessionId = default)
    {
        WebhookSession webhookSession;
        List<CallbackRequestModel> callbacks;

        using var storeSession = await store.OpenSessionAsync(new Marten.Services.SessionOptions());

        if (webhookSessionId == default)
        {
            logger.LogInformation("Creating webhook session id: @WebhookSessionId",
                webhookSessionId);

            webhookSession = new WebhookSession
            {
                StartDate = DateTime.UtcNow,
            };

            storeSession.Store(webhookSession);
            await storeSession.SaveChangesAsync();

            callbacks = new List<CallbackRequestModel>();
        }
        else
        {
            webhookSession = await storeSession.LoadAsync<WebhookSession>(webhookSessionId);

            var results = await storeSession.Query<CallbackRequestModel>()
            .Where(q => q.WebhookSessionId == webhookSessionId)
            .ToListAsync();

            callbacks = results.ToList();
        }

        return new WebhookSessionView
        {
            StartDate = webhookSession.StartDate,
            WebhookSessionId = webhookSession.Id,
            CallRequests = callbacks
        };
    }
}