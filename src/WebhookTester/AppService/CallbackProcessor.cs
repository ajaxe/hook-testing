using ApogeeDev.WebhookTester.Common.Commands;
using ApogeeDev.WebhookTester.Common.Models;
using Marten;

namespace ApogeeDev.WebhookTester.AppService;

public interface ICallbackProcessor
{
    Task Handle(object command);
}

internal class CallbackProcessor : ICallbackProcessor
{
    private readonly IDocumentStore store;
    private readonly ILogger<CallbackProcessor> logger;

    public CallbackProcessor(IDocumentStore store, ILogger<CallbackProcessor> logger)
    {
        this.store = store;
        this.logger = logger;
    }

    public async Task Handle(object command)
    {
        if (command is CallbackReceived callback)
        {
            await HandleCallbackReceived(callback);
        }
        else
        {
            logger.LogWarning("Un-supported command type: " + command?.GetType().Name);
        }
    }

    private async Task HandleCallbackReceived(CallbackReceived callback)
    {
        using var storeSession = await store.OpenSessionAsync(new Marten.Services.SessionOptions());

        var webhookSession = await storeSession.LoadAsync<WebhookSession>(callback.WebhookSessionId);

        if (webhookSession is null)
        {
            logger.LogInformation("Webhook session id: @WebhookSessionId does not exist, ignoring callback event",
                callback.WebhookSessionId);
            return;
        }

        var callbackModel = new CallbackRequestModel
        {
            Headers = callback.Headers,
            RequestBody = callback.RequestBody,
            WebhookSessionId = webhookSession.Id,
        };

        storeSession.Store(callbackModel);

        await storeSession.SaveChangesAsync();
    }
}