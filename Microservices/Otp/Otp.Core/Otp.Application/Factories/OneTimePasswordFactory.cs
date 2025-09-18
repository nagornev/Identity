using Microsoft.Extensions.Options;
using Otp.Application.Abstractions.Factories;
using Otp.Application.Abstractions.Providers;
using Otp.Application.Options;
using Otp.Domain.Aggregates;
using Otp.Domain.ValueObjects;

namespace Otp.Application.Factories
{
    public class OneTimePasswordFactory : IOneTimePasswordFactory
    {
        private readonly ISecretProvider _secretProvider;

        private readonly ITimeProvider _timeProvider;

        private readonly OneTimePasswordOptions _oneTimePasswordOptions;

        public OneTimePasswordFactory(ISecretProvider secretProvider,
                                      ITimeProvider timeProvider,
                                      IOptions<OneTimePasswordOptions> oneTimePasswordOptions)
        {
            _secretProvider = secretProvider;
            _timeProvider = timeProvider;
            _oneTimePasswordOptions = oneTimePasswordOptions.Value;
        }

        public OneTimePassword Create(Guid userId, Channel channel, string tag, string? payload = "")
        {
            long createdAt = _timeProvider.NowUnix();

            return OneTimePassword.Create(userId,
                                          channel,
                                          _secretProvider.Create(),
                                          tag,
                                          createdAt,
                                          createdAt + _oneTimePasswordOptions.Lifetime,
                                          payload);
        }
    }
}
