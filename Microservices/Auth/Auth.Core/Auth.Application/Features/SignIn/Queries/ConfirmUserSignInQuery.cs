using Auth.Application.DTOs;

namespace Auth.Application.Features.SignIn.Queries
{
    public class ConfirmUserSignInQuery : ResultTRequest<TokenPair>
    {
        public ConfirmUserSignInQuery(string otpToken,
                                      string otp,
                                      string audience, 
                                      string publicKey,
                                      string device,
                                      string ipAddress)
        {
            OtpToken = otpToken;
            Otp = otp;
            Audience = audience;
            PublicKey = publicKey;
            Device = device;
            IpAddress = ipAddress;
        }

        public string OtpToken { get; }

        public string Otp { get; }

        public string Audience { get; }

        public string PublicKey { get; }

        public string Device { get; }

        public string IpAddress { get; }
    }
}
