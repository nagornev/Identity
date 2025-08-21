using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Providers.Tokens
{
    public interface IChannelTokenProvider
    {
        /// <summary>
        /// Creates channel token.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="keyPair"></param>
        /// <returns></returns>
        string Create(ChannelTokenCreationParameters parameters, KeyPair keyPair);
    }
}
