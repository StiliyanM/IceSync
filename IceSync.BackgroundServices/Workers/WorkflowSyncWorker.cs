using IceSync.Application.Commands;
using IceSync.BackgroundServices.Settings;
using MediatR;
using Microsoft.Extensions.Options;

namespace IceSync.BackgroundServices.Workers;

public class WorkflowSyncWorker : BackgroundService
{
    private readonly ILogger<WorkflowSyncWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptionsMonitor<SyncSettings> _syncSettingsMonitor;

    public WorkflowSyncWorker(
        ILogger<WorkflowSyncWorker> logger,
        IServiceProvider serviceProvider,
        IOptionsMonitor<SyncSettings> syncSettingsMonitor)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _syncSettingsMonitor = syncSettingsMonitor;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Workflow sync service is running.");

            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            try
            {
                await mediator.Send(new SyncWorkflowsCommand(), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while syncing workflows.");
            }

            await Task.Delay(TimeSpan.FromMinutes(_syncSettingsMonitor.CurrentValue.IntervalInMinutes), stoppingToken);
        }
    }
}