using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Validators;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Application.Validators
{
    public class PasswordValidator : IPasswordValidator
    {
        private readonly IPasswordHashProvider _passwordHashProvider;

        public PasswordValidator(IPasswordHashProvider hashProvider)
        {
            _passwordHashProvider = hashProvider;
        }

        public bool Verify(string password, string hash)
        {
            return CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(_passwordHashProvider.Hash(password)),
                                                           Encoding.UTF8.GetBytes(hash));
        }
    }
}
