namespace Auth.Application.Features.SignUp.Commands
{
    public class ConfirmUserSignUpCommand : ResultRequest
    {
        public ConfirmUserSignUpCommand(string channelToken)
        {
            ChannelToken = channelToken;
        }

        public string ChannelToken { get; }
    }
}
