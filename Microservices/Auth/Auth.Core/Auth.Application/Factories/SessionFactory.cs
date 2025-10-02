using Auth.Application.Abstractions.Factories;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Options;
using Auth.Domain.Aggregates;
using Microsoft.Extensions.Options;

namespace Auth.Application.Factories
{
    public class SessionFactory : ISessionFactory
    {
        private readonly ITimeProvider _timeProvider;

        private RefreshTokenOptions _options;

        public SessionFactory(ITimeProvider timeProvider,
                              IOptions<RefreshTokenOptions> options)
        {
            _timeProvider = timeProvider;
            _options = options.Value;
        }

        public Session Create(Guid userId, Guid kid, string publicKey, string audience, string device, string ipAddress)
        {
            long createdAt = _timeProvider.NowUnix();

            return Session.Create(userId,
                                  kid,
                                  publicKey,
                                  audience,
                                  device,
                                  ipAddress,
                                  createdAt,
                                  createdAt + _options.Lifetime);
        }
    }
}
