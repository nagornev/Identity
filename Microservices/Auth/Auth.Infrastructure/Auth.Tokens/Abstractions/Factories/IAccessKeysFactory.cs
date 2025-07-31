using Auth.Application.Abstractions.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Tokens.Abstractions.Factories
{
    public interface IAccessKeysFactory : Application.Abstractions.Factories.IAccessKeysFactory, IKeysFactory<RSA>
    {
    }
}
