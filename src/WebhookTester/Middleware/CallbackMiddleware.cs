using System.Text;
using ApogeeDev.WebhookTester.AppService;
using ApogeeDev.WebhookTester.Common.Commands;
using ApogeeDev.WebhookTester.Common.Models;
using Microsoft.AspNetCore.Routing.Template;

namespace ApogeeDev.WebhookTester.Middlewares;

public class CallbackHandlerMiddleware
{
    private readonly RequestDelegate next;
    private readonly TemplateMatcher routeMatcher;

    public CallbackHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
        RouteTemplate routeTemplate = TemplateParser.Parse("/callback/{callbackId}");
        this.routeMatcher = new TemplateMatcher(routeTemplate, null);
    }
    public async Task InvokeAsync(HttpContext context, ICallbackProcessor callbackProcessor)
    {
        var uri = context.Request.Path.ToUriComponent();

        var values = new RouteValueDictionary();

        if (routeMatcher.TryMatch(uri, values)
            && Guid.TryParse(values["callBackId"]?.ToString(), out Guid callbackId))
        {
            await callbackProcessor.Handle(await CreateCommand(context, callbackId));

            context.Response.StatusCode = StatusCodes.Status204NoContent;
            return;
        }

        await next(context);
    }

    private async Task<CallbackReceived> CreateCommand(HttpContext context,
        Guid callbackId)
    {
        using StreamReader reader = new StreamReader(context.Request.Body,
            Encoding.UTF8);
        var command = new CallbackReceived
        {
            WebhookSessionId = callbackId,
            Headers = context.Request.Headers
                .ToDictionary(h => h.Key,
                elem => elem.Value.ToList()),
            RequestBody = await reader.ReadToEndAsync(),
            RequestMethod = context.Request.Method,
            QueryString = context.Request.Query,
        };

        if (context.Request.HasFormContentType)
        {
            command.FormData = context.Request.Form;
            command.Files = context.Request.Form?.Files?.Select(f => new FormFileData
            {
                FileName = f.FileName,
                Name = f.Name,
                ContentDisposition = f.ContentDisposition,
                ContentType = f.ContentType,
                Length = f.Length
            }).ToArray();
        }

        return command;
    }
}

public static class CallbackMiddlewareExtensions
{
    public static IApplicationBuilder UseCallbackHandler(this IApplicationBuilder builder)
    {
        return builder.UseWhen(httpContext => httpContext.Request.Path.StartsWithSegments("/callback"),
            builder => builder.UseMiddleware<CallbackHandlerMiddleware>());
    }
}
