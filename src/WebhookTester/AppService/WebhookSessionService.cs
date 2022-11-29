using ApogeeDev.WebhookTester.Common.Models;
using ApogeeDev.WebhookTester.Common.ViewModels;
using Marten;

namespace ApogeeDev.WebhookTester.AppService;

public interface IWebhookSessionService
{
    Task<CallbackRequestModel?> GetCallback(Guid callbackId);
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

    public async Task<CallbackRequestModel?> GetCallback(Guid callbackId)
    {
        if (callbackId == default)
        {
            return null;
        }
        var querySession = store.QuerySession();

        var callback = await querySession.Query<CallbackRequestModel>()
        .Where(q => q.Id == callbackId)
        .FirstOrDefaultAsync();

        return callback;
    }

    public async Task<WebhookSessionView?> GetWebhookSession(Guid webhookSessionId = default)
    {
        WebhookSession webhookSession;
        List<CallbackRequestListItem> callbacks;
        CallbackRequestModel? latestCallback = null;

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

            callbacks = new List<CallbackRequestListItem>();
        }
        else
        {
            webhookSession = await storeSession.LoadAsync<WebhookSession>(webhookSessionId);

            if (webhookSession is null)
            {
                return null;
            }

            var results = await storeSession.Query<CallbackRequestModel>()
            .Where(q => q.WebhookSessionId == webhookSessionId)
            .OrderByDescending(q => q.ReceivedDate)
            .ToListAsync();

            callbacks = results
                .Select(r => new CallbackRequestListItem
                {
                    Id = r.Id,
                    ReceivedDate = r.ReceivedDate,
                }).ToList();

            latestCallback = results.FirstOrDefault();
        }

        return new WebhookSessionView
        {
            StartDate = webhookSession.StartDate,
            WebhookSessionId = webhookSession.Id,
            CallRequests = callbacks,
            MostRecentCallback = latestCallback
        };
    }
}