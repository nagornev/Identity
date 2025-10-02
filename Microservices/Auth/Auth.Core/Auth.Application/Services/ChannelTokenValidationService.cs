using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Applications.Security;

namespace Auth.Application.Services
{
    public class ChannelTokenValidationService : IChannelTokenValidationService
    {
        private readonly IChannelTokenValidator _channelTokenValidator;

        private readonly IChannelTokenPayloadProvider _channelTokenPayloadProvider;

        public ChannelTokenValidationService(IChannelTokenValidator channelTokenValidator,
                                             IChannelTokenPayloadProvider channelTokenPayloadProvider)
        {
            _channelTokenValidator = channelTokenValidator;
            _channelTokenPayloadProvider = channelTokenPayloadProvider;
        }

        public ChannelTokenPayload Validate(string channelToken, KeyPair key, string tag)
        {
            if (!_channelTokenValidator.Validate(channelToken, key, out IReadOnlyDictionary<string, string> claims))
                throw new ChannelTokenInvalidApplicationException(channelToken);

            ChannelTokenPayload payload = _channelTokenPayloadProvider.GetPayload(claims);

            if (payload.Tag != tag)
                throw new ChannelTokenInvalidApplicationException(channelToken);

            return payload;
        }
    }
}
