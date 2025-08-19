using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Validators.Tokens
{
    public interface ITokenValidator
    {
        bool Validate(string token, KeyPair keyPair, out IReadOnlyDictionary<string, string> claims);
    }
}
