using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Providers.Tokens
{
    public interface IRefreshTokenProvider
    {
        /// <summary>
        /// Creates refresh token>.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="keyPair"></param>
        /// <returns></returns>
        string Create(RefreshTokenCreationParameters parameters, KeyPair keyPair);
    }
}
