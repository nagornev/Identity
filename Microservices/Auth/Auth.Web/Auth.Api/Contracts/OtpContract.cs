namespace Auth.Api.Contracts
{
    public class OtpContract
    {
        public OtpContract(Guid otpId,
                           string otp)
        {
            OtpId = otpId;
            Otp = otp;
        }

        public Guid OtpId { get; }

        public string Otp { get; }
    }
}
