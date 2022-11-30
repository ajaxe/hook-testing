using ApogeeDev.WebhookTester;
using ApogeeDev.WebhookTester.AppService;
using ApogeeDev.WebhookTester.Common.Configuration;
using ApogeeDev.WebhookTester.Common.Models;
using Marten;
using Marten.Schema.Identity;
using Oakton;
using Weasel.Core;

const string EnvVarPrefix = "APP_";

var builder = WebApplication.CreateBuilder(args);

builder.Host.ApplyOaktonExtensions();

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

StartupInitializers.ConfigureInjectableServices(builder.Services);

StartupInitializers.ConfigureGoogleAuth(builder.Services, appOptions);

var app = builder.Build();

string appPrefix = Environment.GetEnvironmentVariable($"{EnvVarPrefix}AppPathPrefix") ?? string.Empty;

if (!string.IsNullOrWhiteSpace(appPrefix))
{
    app.Use((context, next) =>
    {
        context.Request.PathBase = appPrefix;
        return next();
    });
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// using reverse proxy for SSL termination
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

return await app.RunOaktonCommands(args);
