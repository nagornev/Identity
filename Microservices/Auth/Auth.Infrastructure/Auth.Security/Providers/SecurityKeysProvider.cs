using Auth.Application.DTOs;
using Auth.Security.Abstractions.Providers;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Security.Providers
{
    public class SecurityKeysProvider : ISecurityKeysProvider
    {
        private readonly IReadOnlyDictionary<string, ISecurityKeyProvider> _providers;

        public SecurityKeysProvider(IEnumerable<ISecurityKeyProvider> providers)
        {
            _providers = providers.ToDictionary(k => k.GetHandableAlgorithm(), v => v);
        }

        public SecurityKey CreateSign(KeyPair keyPair)
        {
            return _providers.TryGetValue(keyPair.Algorithm, out var provider) ?
                    provider.CreateSign(keyPair) :
                    throw new NotSupportedException("Unsupported algorithm type.");
        }

        public SecurityKey CreateVerify(KeyPair keyPair)
        {
            return _providers.TryGetValue(keyPair.Algorithm, out var provider) ?
                     provider.CreateVerify(keyPair) :
                     throw new NotSupportedException("Unsupported algorithm type.");
        }
    }
}
