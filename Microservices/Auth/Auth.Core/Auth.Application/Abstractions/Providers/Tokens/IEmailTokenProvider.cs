using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Providers.Tokens
{
    public interface IEmailTokenProvider
    {
        /// <summary>
        /// Creates email token for <paramref name="userId"/>.
        /// </summary>
        /// <param name="emailKeyPair"></param>
        /// <param name="userId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<string> CreateAsync(KeyPair emailKeyPair, Guid userId, CancellationToken cancellation = default);
    }
}
