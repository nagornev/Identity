using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Options;
using Microsoft.Extensions.Options;

namespace Auth.Keys.Factories
{
    public class RefreshKeyFactory : RsaKeyFactory<RefreshKeyOptions>, IRefreshKeyFactory
    {
        public RefreshKeyFactory(IOptions<RefreshKeyOptions> options,
                                 ITimeProvider timeProvider)
            : base(options, timeProvider)
        {
        }
    }
}
