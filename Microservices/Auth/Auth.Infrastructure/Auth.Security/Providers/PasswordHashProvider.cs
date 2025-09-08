using Auth.Application.Abstractions.Providers;
using Auth.Security.Options;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

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
