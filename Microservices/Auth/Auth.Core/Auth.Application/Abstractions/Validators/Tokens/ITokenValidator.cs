using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Validators.Tokens
{
    public interface ITokenValidator
    {
        Task<bool> ValidateAsync(string token, KeyPair keyPair, CancellationToken cancellation = default);
    }
}
