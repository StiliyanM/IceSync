using IceSync.Infrastructure.Interfaces;
using IceSync.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;

namespace IceSync.Infrastructure.Services;

public class AuthenticatorService : IAuthenticatorService
{
    private readonly IOptionsMonitor<UniversalLoaderApiSettings> _apiSettingsMonitor;
    private readonly HttpClient _httpClient;
    private string? _cachedToken;

    public AuthenticatorService(HttpClient httpClient, IOptionsMonitor<UniversalLoaderApiSettings> apiSettingsMonitor)
    {
        _httpClient = httpClient;
        _apiSettingsMonitor = apiSettingsMonitor;
    }

    public async Task<string> AuthenticateAsync(CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(_cachedToken))
        {
            return _cachedToken;
        }

        var settings = _apiSettingsMonitor.CurrentValue;
        var requestData = new
        {
            apiCompanyId = settings.ApiCompanyId,
            apiUserId = settings.ApiUserId,
            apiUserSecret = settings.ApiUserSecret
        };

        var response = await _httpClient
            .PostAsync(settings.AuthenticateEndpoint, 
            new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json"), 
            cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            _cachedToken = await response.Content.ReadAsStringAsync(cancellationToken);
            return _cachedToken;
        }

        throw new Exception($"Failed to authenticate: {response.ReasonPhrase}");
    }

    public void ClearTokenCache()
    {
        _cachedToken = null;
    }
}
