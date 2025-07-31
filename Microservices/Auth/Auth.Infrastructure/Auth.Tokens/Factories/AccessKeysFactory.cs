using Auth.Application.Abstractions.Providers;
using Auth.Application.DTOs;
using Auth.Tokens.Abstractions.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Tokens.Factories
{
    public class AccessKeysFactory : RsaKeysFactory, IAccessKeysFactory
    {
        public AccessKeysFactory(ITimeProvider timeProvider) 
            : base(timeProvider)
        {
        }
    }
}
