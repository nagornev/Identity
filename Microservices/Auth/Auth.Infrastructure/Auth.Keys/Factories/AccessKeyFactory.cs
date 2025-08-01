using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Options;
using Microsoft.Extensions.Options;

namespace Auth.Keys.Factories
{
    public class AccessKeyFactory : RsaKeyFactory<AccessKeyOptions>, IAccessKeyFactory
    {
        public AccessKeyFactory(IOptions<AccessKeyOptions> options,
                                ITimeProvider timeProvider)
            : base(options, timeProvider)
        {
        }
    }
}
