namespace IceSync.Infrastructure.Settings;

public class UniversalLoaderApiSettings
{
    public required string BaseApiUrl { get; set; }

    public required string AuthenticateEndpoint { get; set; }

    public required string WorkflowsEndpoint { get; set; }

    public required string ApiCompanyId { get; set; }

    public required string ApiUserId { get; set; }

    public required string ApiUserSecret { get; set; }
}
