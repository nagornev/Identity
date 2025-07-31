using Auth.Application.Abstractions.Providers;
using Auth.Application.DTOs;
using Auth.Tokens.Abstractions.Factories;
using System.Security.Cryptography;

namespace Auth.Tokens.Factories
{
    public class EmailKeyFactory : RsaKeysFactory, IEmailKeysFactory
    {
        public EmailKeyFactory(ITimeProvider timeProvider) 
            : base(timeProvider)
        {
        }
    }
}
