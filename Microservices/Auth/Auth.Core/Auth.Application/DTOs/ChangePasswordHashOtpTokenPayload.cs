namespace Auth.Application.DTOs
{
    public class ChangePasswordHashOtpTokenPayload
    {
        public ChangePasswordHashOtpTokenPayload(Guid version)
        {
            Version = version;
        }

        public Guid Version { get; }
    }
}
