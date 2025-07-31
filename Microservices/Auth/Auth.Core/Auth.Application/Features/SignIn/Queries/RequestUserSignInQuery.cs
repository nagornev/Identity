namespace Auth.Application.Features.SignIn.Queries
{
    public class RequestUserSignInQuery : ResultTRequest<string>
    {
        public RequestUserSignInQuery(string emailAddress,
                                      string password)
        {
            EmailAddress = emailAddress;
            Password = password;
        }

        public string EmailAddress { get; }

        public string Password { get; }
    }
}
