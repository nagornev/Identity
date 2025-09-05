using Auth.Application.Abstractions.Factories;
using Auth.Application.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Factories
{
    public class SaltFactory : ISaltFactory
    {
        private readonly SaltOptions _saltOptions;

        public SaltFactory(IOptions<SaltOptions> saltOptions)
        {
            _saltOptions = saltOptions.Value;
        }

        public string Create()
        {
            byte[] salt = new byte[_saltOptions.Size];

            RandomNumberGenerator.Fill(salt);

            return Convert.ToBase64String(salt);
        }
    }
}
