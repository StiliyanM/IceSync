namespace IceSync.Infrastructure.Interfaces;

public interface IAuthenticatorService
{
    Task<string> AuthenticateAsync(CancellationToken cancellationToken);

    void ClearTokenCache();
}
