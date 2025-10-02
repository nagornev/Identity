using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Notification.Application.Options;

namespace Notification.Api.Services
{
    public class JwksConfigurationManager : IConfigurationManager<OpenIdConnectConfiguration>
    {
        private readonly ApplicationOptions _applicationOptions;

        private readonly HttpClient _httpClient;

        private readonly SemaphoreSlim _sync = new SemaphoreSlim(1, 1);

        private OpenIdConnectConfiguration _configuration;

        private bool _isRefreshRequested = true;

        public JwksConfigurationManager(IOptions<ApplicationOptions> applicationOptions)
        {
            _applicationOptions = applicationOptions.Value;
            _httpClient = new HttpClient();
            _configuration = new OpenIdConnectConfiguration();
        }


        public async Task<OpenIdConnectConfiguration> GetConfigurationAsync(CancellationToken cancel)
        {
            if (!_isRefreshRequested)
                return _configuration;

            await _sync.WaitAsync(cancel);
            try
            {
                if (!_isRefreshRequested)
                    return _configuration;

                var newConfiguration = new OpenIdConnectConfiguration();

                var jwksResponse = await _httpClient.GetAsync(_applicationOptions.JwksUrl);
                JsonWebKeySet jwks = new JsonWebKeySet(await jwksResponse.Content.ReadAsStringAsync());

                foreach (var key in jwks.Keys)
                {
                    newConfiguration.SigningKeys.Add(key);
                }

                _configuration = newConfiguration;
                _isRefreshRequested = false;

                return _configuration;
            }
            finally
            {
                _sync.Release();
            }
        }

        public void RequestRefresh()
        {
            _isRefreshRequested = true;
        }
    }
}
