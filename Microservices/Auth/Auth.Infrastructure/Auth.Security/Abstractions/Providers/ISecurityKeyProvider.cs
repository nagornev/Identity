using Auth.Application.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Security.Abstractions.Providers
{
    public interface ISecurityKeyProvider
    {
        string GetHandableAlgorithm();

        SecurityKey CreateSign(KeyPair keyPair);

        SecurityKey CreateVerify(KeyPair keyPair);
    }
}
