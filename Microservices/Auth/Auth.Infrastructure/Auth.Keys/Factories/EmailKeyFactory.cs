using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Options;
using Microsoft.Extensions.Options;

namespace Auth.Keys.Factories
{
    public class EmailKeyFactory : RsaKeyFactory<EmailKeyOptions>, IEmailKeyPairFactory
    {
        public EmailKeyFactory(IOptions<EmailKeyOptions> options,
                               ITimeProvider timeProvider)
            : base(options, timeProvider)
        {
        }
    }
}
