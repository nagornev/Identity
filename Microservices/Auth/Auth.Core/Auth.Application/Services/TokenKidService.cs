using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Exceptions.Applications.Security;

namespace Auth.Application.Services
{
    public class TokenKidService : ITokenKidService
    {
        private readonly ITokenKidProvider _tokenKidProvider;

        public TokenKidService(ITokenKidProvider tokenKidProvider)
        {
            _tokenKidProvider = tokenKidProvider;
        }

        public Guid GetTokenKid(string token)
        {
            return _tokenKidProvider.Get(token)??
                   throw new TokenKidNullApplicationException(token);
        }
    }
}
