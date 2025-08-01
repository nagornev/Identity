using Auth.Application.DTOs;
using Auth.Keys.Abstractions.Providers;
using Auth.Keys.Builders;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Auth.Keys.Providers
{
    public class RsaSecurityKeyProvider : ISecurityKeyProvider
    {
        public bool CanHandle(string algorithm) => algorithm == SecurityAlgorithms.RsaSha256;

        public SecurityKey Create(KeyPair key)
        {
            RSA rsa = RsaBuilder.FromKeys(key.PrivateKey, key.PublicKey);

            return new RsaSecurityKey(rsa)
            {
                KeyId = key.Kid.ToString()
            };
        }
    }
}
