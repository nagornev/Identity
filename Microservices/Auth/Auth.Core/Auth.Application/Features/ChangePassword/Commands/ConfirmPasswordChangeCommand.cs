namespace Auth.Application.Features.ChangePassword.Commands
{
    public class ConfirmPasswordChangeCommand : ResultRequest
    {
        public ConfirmPasswordChangeCommand(string otpToken, string otp)
        {
            OtpToken = otpToken;
            Otp = otp;
        }

        public string OtpToken { get; }

        public string Otp { get; }
    }
}
