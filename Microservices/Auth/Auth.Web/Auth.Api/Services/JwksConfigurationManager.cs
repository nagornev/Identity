using Auth.Application.Abstractions.Storages;
using Auth.Application.Options;
using Auth.Security.Abstractions.Providers;
using Auth.Security.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Auth.Api.Services
{
    public class JwksConfigurationManager : IConfigurationManager<OpenIdConnectConfiguration>
    {
        private readonly IAccessKeyStorage _accessKeyStorage;

        private readonly ISecurityKeysProvider _securityKeysProvider;

        private readonly SemaphoreSlim _sync = new SemaphoreSlim(1, 1);

        private OpenIdConnectConfiguration _configuration;

        private bool _isRefreshRequested = true;

        public JwksConfigurationManager(IAccessKeyStorage accessKeyStorage,
                                        ISecurityKeysProvider securityKeysProvider,
                                        IOptions<ApplicationOptions> applicationOptions)
        {
            _accessKeyStorage = accessKeyStorage;
            _securityKeysProvider = securityKeysProvider;
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

                var keys = await _accessKeyStorage.GetSecurityKeysAsync(_securityKeysProvider);
                foreach (var key in keys)
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
