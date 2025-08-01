using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.DTOs;
using Auth.Application.Options;
using Auth.Keys.Builders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Keys.Factories
{
    public abstract class RsaKeyFactory<TKeyOptionsType> : IKeyFactory
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

        public KeyPairDto Create()
        {
            Guid kid = Guid.NewGuid();
            RsaBuilder.CreateKeys(_options.Size, out byte[] privateKey, out byte[] publicKey);
            long createdAt = _timeProvider.NowUnix();
            long expiresAt = createdAt + _options.TimeToLive;

            return new KeyPairDto(kid,
                                  SecurityAlgorithms.RsaSha256,
                                  privateKey,
                                  publicKey,
                                  createdAt,
                                  expiresAt);
        }
    }
}
