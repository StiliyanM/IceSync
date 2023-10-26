using IceSync.Domain.Interfaces;
using IceSync.Domain.Services;
using IceSync.Infrastructure.Http;
using IceSync.Infrastructure.Repositories;
using IceSync.Infrastructure.Services;
using IceSync.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IceSync.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, IConfiguration configuration)
            => services
            .AddDatabase(configuration)
            .AddSettings(configuration)
            .AddAuthenticatorService()
            .AddUniversalLoaderServices()
            .AddUniversalLoaderHttpClient(configuration)
            .AddRepositories();

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("IceSyncConnectionString");

            services.AddDbContext<WorkflowDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }

        private static IServiceCollection AddSettings(
            this IServiceCollection services, IConfiguration configuration)
            => services.Configure<UniversalLoaderApiSettings>(
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

        private static IServiceCollection AddRepositories(this IServiceCollection services)
            => services.AddScoped<IWorkflowRepository, WorkflowRepository>();

        private static IServiceCollection AddAuthenticatorService(this IServiceCollection services)
            => services.AddTransient<IAuthenticatorService, AuthenticatorService>();
    }
}
