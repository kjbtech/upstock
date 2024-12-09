using Hangfire;
using Hangfire.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UpStock.Workers.Hangfire;

internal class LaunchHangfire : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger _logger;
    private BackgroundJobServer? _hangfireServer;

    public LaunchHangfire(
        ILogger<LaunchHangfire> logger,
        IServiceScopeFactory serviceScopeFactory)
        : base()
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!stoppingToken.IsCancellationRequested)
        {
            GlobalConfiguration.Configuration.UseSimpleAssemblyNameTypeSerializer()
                                .UseRecommendedSerializerSettings()
                                .UseInMemoryStorage()
                                .UseConsole()
                                .UseColouredConsoleLogProvider();

            BackgroundJobServerOptions backgroundJobServerOptions = new BackgroundJobServerOptions
            {
                Activator = new HangfireActivator(_serviceScopeFactory),
                ServerName = "Storia Mundi - Espace client"
            };

            // Create an instance of Hangfire Server and start it.
            _hangfireServer = new BackgroundJobServer(backgroundJobServerOptions);
            _logger.LogInformation("Lancement du service hangfire.");
        }

        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _hangfireServer?.Dispose();

        _logger.LogInformation("Arrêt du service hangfire.");

        return base.StopAsync(cancellationToken);
    }
}
