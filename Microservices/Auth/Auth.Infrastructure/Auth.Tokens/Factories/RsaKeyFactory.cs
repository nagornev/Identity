using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.DTOs;
using Auth.Application.Options;
using Auth.Security.Builders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Security.Factories
{
    public abstract class RsaKeyFactory<TKeyOptionsType> : IKeyPairFactory
        where TKeyOptionsType : KeyOptions
    {
        private readonly TKeyOptionsType _options;

        private readonly ITimeProvider _timeProvider;

        public RsaKeyFactory(IOptions<TKeyOptionsType> options,
                             ITimeProvider timeProvider)
        {
            _options = options.Value;
            _timeProvider = timeProvider;
        }

        public KeyPair Create()
        {
            Guid kid = Guid.NewGuid();
            RsaBuilder.CreateKeys(_options.Size, out byte[] privateKey, out byte[] publicKey);
            long createdAt = _timeProvider.NowUnix();
            long expiresAt = createdAt + _options.TimeToLive;

            return new KeyPair(kid,
                                  SecurityAlgorithms.RsaSha256,
                                  privateKey,
                                  publicKey,
                                  createdAt,
                                  expiresAt);
        }
    }
}
