namespace Auth.Application.Features.ChangeEmailAddress.Commands
{
    public class ConfirmEmailAddressChangeCommand : ResultRequest
    {
        public ConfirmEmailAddressChangeCommand(Guid otpId,
                                                string opt)
        {
            OtpId = otpId;
            Opt = opt;
        }

        public Guid OtpId { get; }

        public string Opt { get; }
    }
}
