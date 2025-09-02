using Auth.Application.DTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Security.Abstractions.Providers
{
    public interface ISecurityKeyProvider
    {
        string GetHandableAlgorithm();

        SecurityKey CreateSign(KeyPair keyPair);

        SecurityKey CreateVerify(KeyPair keyPair);
    }
}
