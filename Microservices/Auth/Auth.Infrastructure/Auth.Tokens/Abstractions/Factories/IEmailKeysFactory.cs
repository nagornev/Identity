using System.Security.Cryptography;

namespace Auth.Tokens.Abstractions.Factories
{
    public interface IEmailKeysFactory : Application.Abstractions.Factories.IEmailKeysFactory, IKeysFactory<RSA>
    {
    }
}
