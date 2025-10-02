namespace Auth.Application.Features.ChangePassword.Commands
{
    public class ConfirmPasswordChangeCommand : ResultRequest
    {
        public ConfirmPasswordChangeCommand(Guid otpId, string otp)
        {
            OtpId = otpId;
            Otp = otp;
        }

        public Guid OtpId { get; }

        public string Otp { get; }
    }
}
