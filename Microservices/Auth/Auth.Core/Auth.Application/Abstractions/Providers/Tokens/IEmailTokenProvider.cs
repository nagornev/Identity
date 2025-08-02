using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Providers.Tokens
{
    public interface IEmailTokenProvider
    {
        /// <summary>
        /// Creates email token.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="keyPair"></param>
        /// <returns></returns>
        string Create(EmailTokenCreationParameters parameters, KeyPair keyPair);
    }
}
