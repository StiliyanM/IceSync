using System.Net.Http.Headers;
using System.Net;
using IceSync.Domain.Interfaces;

namespace IceSync.Infrastructure.Http
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private const string BearerScheme = "Bearer";
        private readonly IAuthenticatorService _authenticatorService;

        public AuthenticationHandler(IAuthenticatorService authenticatorService)
        {
            _authenticatorService = authenticatorService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _authenticatorService.AuthenticateAsync(cancellationToken);
            request.Headers.Authorization = new AuthenticationHeaderValue(BearerScheme, token);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _authenticatorService.ClearTokenCache();
                token = await _authenticatorService.AuthenticateAsync(cancellationToken);
                request.Headers.Authorization = new AuthenticationHeaderValue(BearerScheme, token);
                response = await base.SendAsync(request, cancellationToken);
            }

            return response;
        }
    }
}
