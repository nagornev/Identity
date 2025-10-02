using Auth.Application.DTOs;
using Auth.Security.Abstractions.Providers;
using Auth.Security.Builders;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Auth.Security.Providers
{
    public class RsaSecurityKeyProvider : ISecurityKeyProvider
    {
        public string GetHandableAlgorithm()
        {
            return SecurityAlgorithms.RsaSha256;
        }

        public SecurityKey CreateSign(KeyPair keyPair)
        {
            RSA rsa = RsaBuilder.FromPrivateKey(keyPair.PrivateKey);

            return new RsaSecurityKey(rsa)
            {
                KeyId = keyPair.Kid.ToString()
            };
        }

        public SecurityKey CreateVerify(KeyPair keyPair)
        {
            RSA rsa = RsaBuilder.FromPublicKey(keyPair.PublicKey);

            return new RsaSecurityKey(rsa)
            {
                KeyId = keyPair.Kid.ToString()
            };
        }
    }
}
