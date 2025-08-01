using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;
using Auth.Keys.Abstractions.Providers;
using Auth.Tokens.Abstractions.Providers;
using Auth.Tokens.Abstractions.Validators;

namespace Auth.Tokens.Validators
{
    public abstract class JwtTokenValidator<TKeyStorageType>
        where TKeyStorageType : IKeyStorageReader
    {
        private readonly IJwtClaimsProvider _jwtClaimsProvider;

        private readonly IJwtSignatureValidator _jwtSignatureValidator;

        private readonly TKeyStorageType _keyStorage;

        private readonly ISecurityKeyProvider _securityKeyProvider;

        public JwtTokenValidator(IJwtClaimsProvider jwtParser,
                                 IJwtSignatureValidator jwtValidator,
                                 TKeyStorageType keyStorage,
                                 ISecurityKeyProvider securityKeyProvider)
        {
            _jwtClaimsProvider = jwtParser;
            _jwtSignatureValidator = jwtValidator;
            _keyStorage = keyStorage;
            _securityKeyProvider = securityKeyProvider;
        }

        public async Task<bool> ValidateAsync(string token, CancellationToken cancellation = default)
        {
            KeyPair keyPair = await GetKeyPair(token, cancellation);

            return await ValidateAsync(token, keyPair);
        }

        private async Task<KeyPair> GetKeyPair(string token, CancellationToken cancellation)
        {
            Guid kid = _jwtClaimsProvider.GetKid(token);

            return await _keyStorage.GetKeyPairAsync(kid, cancellation);
        }

        private async Task<bool> ValidateAsync(string token, KeyPair keyPair)
        {
            return await _jwtSignatureValidator.ValidateAsync(token, new()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = _securityKeyProvider.Create(keyPair),
            });
        }
    }
}
