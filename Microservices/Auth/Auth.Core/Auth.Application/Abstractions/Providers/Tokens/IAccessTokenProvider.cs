using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Providers.Tokens
{
    public interface IAccessTokenProvider
    {
        /// <summary>
        /// Creates access token.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="keyPair"></param>
        /// <returns></returns>
        string Create(AccessTokenCreationParameters parameters, KeyPair keyPair);
    }
}
