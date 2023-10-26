using IceSync.Application.Extensions;
using IceSync.BackgroundServices.Settings;
using IceSync.BackgroundServices.Workers;
using IceSync.Infrastructure.Extensions;

namespace IceSync.BackgroundServices.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services, IConfiguration configuration)
        => services
            .AddApplicationServices()
            .AddInfrastructureServices(configuration)
            .AddSettings(configuration)
            .AddBackgroundServices();

    private static IServiceCollection AddSettings(
        this IServiceCollection services, IConfiguration configuration)
        => services.Configure<SyncSettings>(configuration.GetSection(nameof(SyncSettings)));

    private static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        => services.AddHostedService<WorkflowSyncWorker>();
}

