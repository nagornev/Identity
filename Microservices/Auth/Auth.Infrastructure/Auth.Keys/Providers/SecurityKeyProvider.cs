using Auth.Application.DTOs;
using Auth.Keys.Abstractions.Providers;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Keys.Providers
{
    public class SecurityKeyProvider : ISecurityKeyProvider
    {
        private readonly IEnumerable<ISecurityKeyProvider> _providers;

        public SecurityKeyProvider(IEnumerable<ISecurityKeyProvider> providers)
        {
            _providers = providers;
        }

        public bool CanHandle(string algorithm)
        {
            return _providers.Any(x => x.CanHandle(algorithm));
        }

        public SecurityKey Create(KeyPair key)
        {
            return _providers.FirstOrDefault(x => x.CanHandle(key.Algorithm))!.Create(key) ??
                              throw new NotSupportedException("Unsupported algorithm type.");
        }
    }
}
