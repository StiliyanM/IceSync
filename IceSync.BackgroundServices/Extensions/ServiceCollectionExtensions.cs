using IceSync.Application.Commands;
using IceSync.BackgroundServices.Settings;
using IceSync.BackgroundServices.Workers;
using IceSync.Infrastructure.Extensions;

namespace IceSync.BackgroundServices.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services, IConfiguration configuration)
        => services
            .AddInfrastructureServices(configuration)
            .AddBackgroundServiceConfigurations(configuration)
            .AddMediatr()
            .AddBackgroundServices();

    private static IServiceCollection AddBackgroundServiceConfigurations(
        this IServiceCollection services, IConfiguration configuration)
        => services.Configure<SyncSettings>(configuration.GetSection(nameof(SyncSettings)));

    private static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        => services.AddHostedService<WorkflowSyncWorker>();

    private static IServiceCollection AddMediatr(this IServiceCollection services)
        => services
        .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SyncWorkflowsCommand).Assembly));
}

