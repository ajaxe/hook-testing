using System.Net;
using ApogeeDev.WebhookTester;
using ApogeeDev.WebhookTester.AppService;
using ApogeeDev.WebhookTester.Common.Configuration;
using ApogeeDev.WebhookTester.Common.Models;
using ApogeeDev.WebhookTester.Middlewares;
using AspNetCoreRateLimit;
using Marten;
using Marten.Schema.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Oakton;
using Serilog;
using Serilog.Formatting.Compact;
using Weasel.Core;

const string EnvVarPrefix = "APP_";

var builder = WebApplication.CreateBuilder(args);

builder.Host.ApplyOaktonExtensions();

Log.Logger = new LoggerConfiguration()
.WriteTo.Console(new CompactJsonFormatter())
.CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddOptions();
builder.Configuration
    .AddJsonFile($"secrets.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables(prefix: EnvVarPrefix);

var appOptions = new AppOptions();
builder.Configuration.GetSection(AppOptions.SectionName).Bind(appOptions);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/User");
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.RequireHeaderSymmetry = false;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddMarten(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MainStore");
    options.Connection(connectionString);

    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
    options.Policies.ForAllDocuments(m =>
    {
        if (m.IdType == typeof(Guid))
        {
            m.IdStrategy = new CombGuidIdGeneration();
        }
    });
    options.RetryPolicy(DefaultRetryPolicy.Twice());

    options.RegisterDocumentTypes(new[] { typeof(WebhookSession),
        typeof(CallbackRequestModel) });
})
.OptimizeArtifactWorkflow();

builder.Services.Configure<IpRateLimitOptions>(
    builder.Configuration.GetSection("IpRateLimiting"));

if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(new DirectoryInfo("/dpapi-keys/"));
}

StartupInitializers.ConfigureInjectableServices(builder.Services);

StartupInitializers.ConfigureGoogleAuth(builder.Services, appOptions);

var app = builder.Build();

app.UseIpRateLimiting();
app.UseForwardedHeaders();
app.UseSerilogRequestLogging();

string appPrefix = Environment.GetEnvironmentVariable($"{EnvVarPrefix}AppPathPrefix") ?? string.Empty;

if (!string.IsNullOrWhiteSpace(appPrefix))
{
    app.Use((context, next) =>
    {
        context.Request.PathBase = appPrefix;
        return next();
    });
}

app.UseExceptionHandler("/Error");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseCallbackHandler();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

return await app.RunOaktonCommands(args);
