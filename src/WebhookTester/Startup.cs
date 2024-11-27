using ApogeeDev.WebhookTester.Common.Configuration;
using ApogeeDev.WebhookTester.Common.Models;
using ApogeeDev.WebhookTester.Middlewares;
using AspNetCoreRateLimit;
using Marten;
using Marten.Events.Daemon.Internals;
using Marten.Exceptions;
using Marten.Schema.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Npgsql;
using Polly;
using Serilog;
using Weasel.Core;

namespace ApogeeDev.WebhookTester;

public class Startup
{
    public const string EnvVarPrefix = "APP_";
    public static string AppPrefix = Environment.GetEnvironmentVariable($"{EnvVarPrefix}AppPathPrefix") ?? string.Empty;
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Env { get; }
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHealthChecks();
        services.AddOptions();

        var appOptions = new AppOptions();
        Configuration.GetSection(AppOptions.SectionName).Bind(appOptions);

        var authOptions = new OAuthOptions();
        Configuration.GetSection(OAuthOptions.SectionName)
            .Bind(authOptions);

        // Add services to the container.
        services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizeFolder("/User");
        });

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.RequireHeaderSymmetry = false;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        services.AddHttpContextAccessor();
        services.AddMarten(options =>
        {
            var connectionString = Configuration.GetConnectionString("MainStore")
                ?? throw new InvalidOperationException($"Invalid 'MainStore' connection string");

            options.Connection(connectionString);

            if (Env.IsDevelopment())
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

            options.ConfigurePolly(builder => builder.AddRetry(new()
            {
                ShouldHandle = new PredicateBuilder()
                                .Handle<NpgsqlException>()
                                .Handle<MartenCommandException>()
                                .Handle<EventLoaderException>(),
                MaxRetryAttempts = 3,
                Delay = TimeSpan.FromMilliseconds(50),
                BackoffType = DelayBackoffType.Exponential
            }));

            options.RegisterDocumentTypes(new[] { typeof(WebhookSession),
                typeof(CallbackRequestModel) });
        })
        .OptimizeArtifactWorkflow();

        services.Configure<IpRateLimitOptions>(
            Configuration.GetSection("IpRateLimiting"));

        if (!Env.IsDevelopment())
        {
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo("/dpapi-keys/"));
        }

        StartupInitializers.ConfigureInjectableServices(services);

        StartupInitializers.ConfigureGoogleAuth(services, authOptions);

    }
    public void Configure(IApplicationBuilder app)
    {
        app.UseIpRateLimiting();
        app.UseForwardedHeaders();
        app.UseSerilogRequestLogging();

        if (!string.IsNullOrWhiteSpace(AppPrefix))
        {
            app.Use((context, next) =>
            {
                context.Request.PathBase = AppPrefix;
                return next();
            });
        }

        app.UseExceptionHandler("/Error");
        // Configure the HTTP request pipeline.
        if (!Env.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStaticFiles();

        app.UseCallbackHandler();
        app.UseCspReportMiddleware();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapHealthChecks("/healthcheck");
        });
    }
}