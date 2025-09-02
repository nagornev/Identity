using Auth.Application.Abstractions.Providers;
using Auth.Security.Options;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Security.Providers
{
    public class PasswordHashProvider : IPasswordHashProvider
    {
        private const int _size = 32;

        private readonly PasswordHashOptions _passwordHashOptions;

        private readonly byte[] _salt;

        public PasswordHashProvider(IOptions<PasswordHashOptions> passwordHashOptions)
        {
            _passwordHashOptions = passwordHashOptions.Value;
            _salt = Encoding.UTF8.GetBytes(_passwordHashOptions.Salt);
        }

        public string Hash(string value)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(value))
            {
                Salt = _salt,
                DegreeOfParallelism = 4,
                MemorySize = 65536,   
                Iterations = 4
            };

            return Encoding.UTF8.GetString(argon2.GetBytes(_size));
        }
    }
}
