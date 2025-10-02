using Microsoft.Extensions.Options;
using Otp.Application.Abstractions.Providers;
using Otp.Application.Options;
using System.Security.Cryptography;

namespace Otp.Application.Providers
{
    public class SecretProvider : ISecretProvider
    {
        private readonly SecretOptions _secretOptions;

        public SecretProvider(IOptions<SecretOptions> secretOptions)
        {
            _secretOptions = secretOptions.Value;
        }

        public string Create()
        {
            byte[] secret = new byte[_secretOptions.Size];

            RandomNumberGenerator.Fill(secret);

            return Convert.ToBase64String(secret);
        }
    }
}
