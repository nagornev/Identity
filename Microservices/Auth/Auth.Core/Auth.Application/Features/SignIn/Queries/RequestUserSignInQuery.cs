using Auth.Application.DTOs;

namespace Auth.Application.Features.SignIn.Queries
{
    public class RequestUserSignInQuery : ResultTRequest<Otp>
    {
        public RequestUserSignInQuery(string emailAddress,
                                      string password,
                                      string audience,
                                      string publicKey,
                                      RequestContext requestContext)
        {
            EmailAddress = emailAddress;
            Password = password;
            Audience = audience;
            PublicKey = publicKey;
            RequestContext = requestContext;
        }

        public string EmailAddress { get; }

        public string Password { get; }

        public string Audience { get; }

        public string PublicKey { get; }

        public RequestContext RequestContext { get; }
    }
}
