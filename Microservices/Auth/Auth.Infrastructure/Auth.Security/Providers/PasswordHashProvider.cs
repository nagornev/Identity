using Auth.Application.Abstractions.Providers;
using Auth.Security.Options;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Security.Providers
{
    public class PasswordHashProvider : IPasswordHashProvider
    {
        private readonly PasswordHashOptions _passwordHashOptions;


        public PasswordHashProvider(IOptions<PasswordHashOptions> passwordHashOptions)
        {
            _passwordHashOptions = passwordHashOptions.Value;
        }

        public string Hash(string value, string salt)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(value),
                                                                      Convert.FromBase64String(salt),
                                                                      _passwordHashOptions.Iterations,
                                                                      HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(_passwordHashOptions.Size);

                return Convert.ToBase64String(hash);
            }
        }
    }
}
