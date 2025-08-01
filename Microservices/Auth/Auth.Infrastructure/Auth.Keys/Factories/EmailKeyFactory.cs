using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.DTOs;
using Auth.Application.Options;
using Auth.Tokens.Abstractions.Factories;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Auth.Keys.Factories
{
    public class EmailKeyFactory : RsaKeyFactory<EmailKeyOptions>, IEmailKeyFactory
    {
        public EmailKeyFactory(IOptions<EmailKeyOptions> options,
                               ITimeProvider timeProvider)
            : base(options, timeProvider)
        {
        }
    }
}
