using Auth.Application.DTOs;

namespace Auth.Application.Features.SignIn.Queries
{
    public class ConfirmUserSignInQuery : ResultTRequest<TokenPair>
    {
        public ConfirmUserSignInQuery(Guid otpId,
                                      string otp)
        {
            OtpId = otpId;
            Otp = otp;
        }

        public Guid OtpId { get; }

        public string Otp { get; }
    }
}
