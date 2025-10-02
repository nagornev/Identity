using Auth.Application.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Security.Abstractions.Providers
{
    public interface ISecurityKeysProvider
    {
        SecurityKey CreateSign(KeyPair keyPair);

        SecurityKey CreateVerify(KeyPair keyPair);
    }
}
