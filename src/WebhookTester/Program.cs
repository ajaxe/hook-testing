using ApogeeDev.WebhookTester.AppService;
using ApogeeDev.WebhookTester.Common.Models;
using Marten;
using Marten.Schema.Identity;
using Oakton;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ApplyOaktonExtensions();

builder.Configuration
    .AddJsonFile($"secrets.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables(prefix: "APP_");

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddTransient<IWebhookSessionService, WebhookSessionService>();
builder.Services.AddTransient<ICallbackProcessor, CallbackProcessor>();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.MapRazorPages();

return await app.RunOaktonCommands(args);
