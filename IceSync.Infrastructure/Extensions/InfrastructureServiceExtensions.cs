using IceSync.Infrastructure.Http;
using IceSync.Infrastructure.Interfaces;
using IceSync.Infrastructure.Services;
using IceSync.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IceSync.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
            => services
            .AddDatabase(configuration)
            .AddSettings(configuration)
            .AddUniversalLoaderServices()
            .AddUniversalLoaderHttpClient(configuration);

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("IceSyncConnectionString");

            services.AddDbContext<WorkflowDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }

        private static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
            => services.Configure<UniversalLoaderApiSettings>(options =>
                configuration.GetSection(nameof(UniversalLoaderApiSettings)));

        private static IServiceCollection AddUniversalLoaderServices(this IServiceCollection services)
        {
            services.AddTransient<AuthenticatorService>();
            services.AddTransient<UniversalLoaderApiClient>();
            services.AddTransient<AuthenticationHandler>();
            return services;
        }

        private static IServiceCollection AddUniversalLoaderHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            var settingsSection = configuration.GetSection(nameof(UniversalLoaderApiSettings)) 
                ?? throw new InvalidOperationException("UniversalLoaderApiSettings section is missing in the configuration.");
            var settings = settingsSection.Get<UniversalLoaderApiSettings>();
            if (settings == null || string.IsNullOrWhiteSpace(settings.BaseApiUrl))
            {
                throw new InvalidOperationException("UniversalLoaderApiSettings configuration is invalid.");
            }

            services.AddHttpClient<IUniversalLoaderApiClient, UniversalLoaderApiClient>(client =>
            {
                client.BaseAddress = new Uri(settings.BaseApiUrl);
            })
            .AddHttpMessageHandler<AuthenticationHandler>();

            return services;
        }
    }

}
