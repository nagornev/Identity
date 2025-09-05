using Microsoft.Extensions.Options;
using Otp.Application.Abstractions.Providers;
using Otp.Application.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
