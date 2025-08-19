using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Services
{
    public interface IChannelTokenValidationService
    {
        ChannelTokenPayload Validate(string channelToken, KeyPair key, string tag);
    }
}
