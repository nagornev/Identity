using Auth.Application.DTOs;

namespace Auth.Application.Features.SignIn.Queries
{
    public class ConfirmUserSignInQuery : ResultTRequest<TokenPair>
    {
        public ConfirmUserSignInQuery(Guid otpId,
                                      string otp,
                                      string newPublicKey,
                                      long timestamp,
                                      string signature)
        {
            OtpId = otpId;
            Otp = otp;
            NewPublicKey = newPublicKey;
            Timestamp = timestamp;
            Signature = signature;
        }

        public Guid OtpId { get; }

        public string Otp { get; }

        public string NewPublicKey { get; }

        public long Timestamp { get; }

        public string Signature { get; }
    }
}
