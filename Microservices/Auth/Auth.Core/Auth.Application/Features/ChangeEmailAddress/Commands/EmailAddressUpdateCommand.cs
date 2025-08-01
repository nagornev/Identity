namespace Auth.Application.Features.ChangeEmailAddress.Commands
{
    public class EmailAddressUpdateCommand : ResultRequest
    {
        public EmailAddressUpdateCommand(string emailToken)
        {
            EmailToken = emailToken;
        }

        public string EmailToken { get; }
    }
}
