using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Tokens.Abstractions.Factories
{
    public interface IKeysFactory<TKeyType>
    {
        TKeyType FromPrivateKey(byte[] privateKey);

        TKeyType FromPublicKey(byte[] publicKey);
    }
}
