using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Options;
using Microsoft.Extensions.Options;

namespace Auth.Security.Factories
{
    public class AccessKeyFactory : RsaKeyFactory<AccessKeyOptions>, IAccessKeyPairFactory
    {
        public AccessKeyFactory(IOptions<AccessKeyOptions> options,
                                ITimeProvider timeProvider)
            : base(options, timeProvider)
        {
        }
    }
}
