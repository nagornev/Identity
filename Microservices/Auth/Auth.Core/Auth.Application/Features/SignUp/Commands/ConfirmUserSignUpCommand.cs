namespace Auth.Application.Features.SignUp.Commands
{
    public class ConfirmUserSignUpCommand : ResultRequest
    {
        public ConfirmUserSignUpCommand(string emailToken)
        {
            EmailToken = emailToken;
        }

        public string EmailToken { get; }
    }
}
