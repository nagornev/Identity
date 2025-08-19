using Auth.Application.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Security.Abstractions.Providers
{
    public interface ISecurityKeyProvider
    {
        bool CanHandle(string algorithm);

        SecurityKey Create(KeyPair keyPair);
    }
}
