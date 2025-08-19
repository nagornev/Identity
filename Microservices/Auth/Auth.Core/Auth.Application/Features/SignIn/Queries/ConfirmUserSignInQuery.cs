using Auth.Application.DTOs;

namespace Auth.Application.Features.SignIn.Queries
{
    public class ConfirmUserSignInQuery : ResultTRequest<TokenPair>
    {
        public ConfirmUserSignInQuery(string otpToken,
                                      string otp,
                                      string newPublicKey,
                                      long timestamp,
                                      string signature)
        {
            OtpToken = otpToken;
            Otp = otp;
            NewPublicKey = newPublicKey;
            Timestamp = timestamp;
            Signature = signature;
        }

        public string OtpToken { get; }

        public string Otp { get; }

        public string NewPublicKey { get; }

        public long Timestamp { get; }

        public string Signature { get; }
    }
}
