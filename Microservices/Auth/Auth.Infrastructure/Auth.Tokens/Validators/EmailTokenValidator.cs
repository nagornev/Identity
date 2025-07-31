using Auth.Application.Abstractions.Storages.Keys;
using Auth.Application.Abstractions.Validators;
using Auth.Application.DTOs;
using Auth.Tokens.Abstractions.Factories;
using Auth.Tokens.Abstractions.Parsers;
using Auth.Tokens.Abstractions.Validators;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Auth.Tokens.Validators
{
    public class EmailTokenValidator : IEmailTokenValidator
    {
        private readonly IJwtParser _jwtParser;

        private readonly IJwtValidator _jwtValidator;

        private readonly IEmailPublicKeyStorage _emailPublicKeyStorage;

        private readonly IEmailKeysFactory _emailKeysFactory;

        public EmailTokenValidator(IJwtParser jwtParser,
                                   IJwtValidator jwtValidator,
                                   IEmailPublicKeyStorage emailPublicKeyStorage,
                                   IEmailKeysFactory emailKeysFactory)
        {
            _jwtParser = jwtParser;
            _jwtValidator = jwtValidator;
            _emailPublicKeyStorage = emailPublicKeyStorage;
            _emailKeysFactory = emailKeysFactory;
        }

        public async Task<bool> ValidateAsync(string token, CancellationToken cancellation = default)
        {
            KeyDto publicKey = await GetPublicKey(token, cancellation);

            return await ValidateAsync(token, publicKey);
        }

        private async Task<KeyDto> GetPublicKey(string token, CancellationToken cancellation)
        {
            Guid kid = _jwtParser.GetKid(token);

            return await _emailPublicKeyStorage.GetAsync(kid, cancellation);
        }

        private async Task<bool> ValidateAsync(string token, KeyDto publicKey)
        {
            using (RSA rsa = _emailKeysFactory.FromPublicKey(publicKey.Key))
            {
                return await _jwtValidator.ValidateAsync(token, new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new RsaSecurityKey(rsa),
                });
            }
        }
    }
}
