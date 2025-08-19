using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Options;
using Microsoft.Extensions.Options;

namespace Auth.Security.Factories
{
    public class RefreshKeyFactory : RsaKeyFactory<RefreshKeyOptions>, IRefreshKeyPairFactory
    {
        public RefreshKeyFactory(IOptions<RefreshKeyOptions> options,
                                 ITimeProvider timeProvider)
            : base(options, timeProvider)
        {
        }
    }
}
