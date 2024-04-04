namespace ApogeeDev.WebhookTester.Middlewares;

public class CspReportMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<CspReportMiddleware> logger;

    public CspReportMiddleware(RequestDelegate next, ILogger<CspReportMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using (var reader = new StreamReader(context.Request.Body))
        {
            logger.LogInformation("{@Report}", await reader.ReadToEndAsync());
        }
        await next(context);
    }
}

public static class CspReportMiddlewareExtensions
{
    public static IApplicationBuilder UseCspReportMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseWhen(httpContext => httpContext.Request.Path.StartsWithSegments("/cspreport"),
            builder => builder.UseMiddleware<CspReportMiddleware>());
    }
}
