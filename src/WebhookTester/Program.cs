using ApogeeDev.WebhookTester;
using Oakton;
using Serilog;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ApplyOaktonExtensions();
builder.Host.UseSerilog();

builder.Configuration
    .AddJsonFile($"secrets.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables(prefix: Startup.EnvVarPrefix);

Log.Logger = new LoggerConfiguration()
.WriteTo.Console(new CompactJsonFormatter())
.CreateLogger();

var startup = new Startup(builder.Configuration, builder.Environment);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app);

return await app.RunOaktonCommands(args);
