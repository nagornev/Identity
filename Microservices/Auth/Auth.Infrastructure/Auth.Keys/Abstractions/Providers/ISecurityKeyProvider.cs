using Auth.Application.DTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Keys.Abstractions.Providers
{
    public interface ISecurityKeyProvider
    {
        bool CanHandle(string algorithm);

        SecurityKey Create(KeyPairDto keyPair);
    }
}
