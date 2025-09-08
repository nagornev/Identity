namespace Auth.Application.DTOs
{
    public class ChangeEmailAddressOtpTokenPayload
    {
        public ChangeEmailAddressOtpTokenPayload(Guid version)
        {
            Version = version;
        }

        public Guid Version { get; }
    }
}
