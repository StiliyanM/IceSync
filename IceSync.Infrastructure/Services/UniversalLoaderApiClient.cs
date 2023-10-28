using IceSync.Domain.Models;
using IceSync.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.Text.Json;
using IceSync.Domain.Interfaces;

namespace IceSync.Domain.Services
{
    public class UniversalLoaderApiClient : IUniversalLoaderApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsMonitor<UniversalLoaderApiSettings> _apiSettingsMonitor;

        public UniversalLoaderApiClient(
            HttpClient httpClient, IOptionsMonitor<UniversalLoaderApiSettings> apiSettingsMonitor)
        {
            _httpClient = httpClient;
            _apiSettingsMonitor = apiSettingsMonitor;

            _httpClient.BaseAddress = new Uri(_apiSettingsMonitor.CurrentValue.BaseApiUrl);
        }

        public async Task<IEnumerable<Workflow>> GetWorkflowsAsync(CancellationToken cancellationToken)
        {
            var settings = _apiSettingsMonitor.CurrentValue;
            string endpointUrl = $"{settings.BaseApiUrl}{settings.WorkflowsEndpoint}";

            var response = await _httpClient.GetAsync(endpointUrl, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<IEnumerable<Workflow>>(jsonString, options) ?? 
                    throw new InvalidOperationException("Failed to deserialize response.");
            }

            throw new Exception($"Error fetching workflows: {response.ReasonPhrase}");
        }

        public async Task<bool> RunWorkflowAsync(int id, CancellationToken cancellationToken)
        {
            var settings = _apiSettingsMonitor.CurrentValue;
            string endpointUrl = $"{settings.BaseApiUrl}{string.Format(settings.RunWorkflowEndpoint, id)}";

            var response = await _httpClient.PostAsync(endpointUrl, null, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
