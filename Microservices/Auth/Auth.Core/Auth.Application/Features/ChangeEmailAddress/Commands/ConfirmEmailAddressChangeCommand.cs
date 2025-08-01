namespace Auth.Application.Features.ChangeEmailAddress.Commands
{
    public class ConfirmEmailAddressChangeCommand : ResultRequest
    {
        public ConfirmEmailAddressChangeCommand(string optToken,
                                                string opt)
        {
            OptToken = optToken;
            Opt = opt;
        }

        public string OptToken { get; }

        public string Opt { get; }
    }
}
