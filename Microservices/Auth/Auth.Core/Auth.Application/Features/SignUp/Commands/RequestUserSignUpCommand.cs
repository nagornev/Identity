namespace Auth.Application.Features.SignUp.Commands
{
    public class RequestUserSignUpCommand : ResultRequest
    {
        public RequestUserSignUpCommand(string emailAddress, string personName, string password)
        {
            EmailAddress = emailAddress;
            PersonName = personName;
            Password = password;
        }

        public string EmailAddress { get; }

        public string PersonName { get; }

        public string Password { get; }
    }
}
