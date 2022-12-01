using ApogeeDev.WebhookTester.Abstractions;
using ApogeeDev.WebhookTester.Common.Configuration;
using ApogeeDev.WebhookTester.Common.Exceptions;
using ApogeeDev.WebhookTester.Common.Models;
using ApogeeDev.WebhookTester.Common.ViewModels;
using Marten;
using Microsoft.Extensions.Options;

namespace ApogeeDev.WebhookTester.AppService;

public interface IWebhookSessionService
{
    Task<CallbackRequestModel?> GetCallback(Guid callbackId);
    Task<WebhookSessionView> GetWebhookSession(Guid webhookSessionId = default);
    Task AssignWebhookSessionToCurrentUser(Guid webhookSessionId);
    Task RemoveWebhookSessionToCurrentUser(Guid webhookSessionId);
}

internal class WebhookSessionService : IWebhookSessionService
{
    private readonly IDocumentStore store;
    private readonly IUserInfoProvider userInfo;
    private readonly ILogger<WebhookSessionService> logger;
    private readonly int maxSessionPerUser;

    private UserProfile currentUser;

    private UserProfile CurrentUser => currentUser ?? (currentUser = userInfo.GetUserProfile());

    public WebhookSessionService(IDocumentStore store,
        IUserInfoProvider userInfo, IOptions<AppOptions> options,
        ILogger<WebhookSessionService> logger)
    {
        this.store = store;
        this.userInfo = userInfo;
        this.logger = logger;
        this.maxSessionPerUser = options?.Value.MaxSessionPerUser
            ?? throw new InvalidOperationException("Invalid config 'MaxSessionPerUser'");
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

        return PrepareCallbackModel(callback);
    }

    private CallbackRequestModel? PrepareCallbackModel(CallbackRequestModel? callback)
    {
        if (callback is null)
        {
            return callback;
        }

        callback.Headers = callback.Headers.Where(kvp => !kvp.Key.ToLower().StartsWith("x-forwarded-"))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

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
            MostRecentCallback = PrepareCallbackModel(latestCallback),
            UserIdentifier = webhookSession?.UserIdentifier,
        };
    }

    public async Task AssignWebhookSessionToCurrentUser(Guid webhookSessionId)
    {
        using var storeSession = await store.OpenSessionAsync(new Marten.Services.SessionOptions());

        var assignedCount = await storeSession.Query<WebhookSession>()
        .CountAsync(q => q.UserIdentifier == CurrentUser.UserIdentifier);

        if (assignedCount > maxSessionPerUser)
        {
            throw new UiException($"Cannot save more than {maxSessionPerUser} sessions to user profile.");
        }

        await UpdateWebhookSessionForCurrentUser(webhookSessionId, setUser: true, storeSession);
    }
    public async Task RemoveWebhookSessionToCurrentUser(Guid webhookSessionId)
    {
        using var storeSession = await store.OpenSessionAsync(new Marten.Services.SessionOptions());
        await UpdateWebhookSessionForCurrentUser(webhookSessionId, setUser: false, storeSession);
    }
    private async Task UpdateWebhookSessionForCurrentUser(Guid webhookSessionId,
        bool setUser, IDocumentSession? storeSession)
    {
        if (webhookSessionId == default)
        {
            return;
        }

        if (CurrentUser is null)
        {
            logger.LogWarning("Invalid current user information, cannot save @{WebhookSessionId}",
                webhookSessionId);
            return;
        }

        var existing = await storeSession.Query<WebhookSession>()
        .Where(q => q.Id == webhookSessionId)
        .FirstOrDefaultAsync();

        if (existing is null)
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(existing.UserIdentifier)
            && existing.UserIdentifier != currentUser.UserIdentifier)
        {
            logger.LogWarning("@{WebhookSessionId} is already assigned to @{User}, @{CurrentUser}",
                webhookSessionId, existing.UserIdentifier, currentUser.UserIdentifier);
            return;
        }

        if (setUser)
        {
            existing.UserIdentifier = currentUser.UserIdentifier;
        }
        else
        {
            existing.UserIdentifier = null;
        }

        storeSession.Store(existing);

        await storeSession.SaveChangesAsync();
    }
}