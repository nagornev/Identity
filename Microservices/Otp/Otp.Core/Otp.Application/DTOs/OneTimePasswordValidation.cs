namespace Otp.Application.DTOs
{
    public class OneTimePasswordValidation
    {
        public OneTimePasswordValidation(bool isValid,
                                         Guid subject,
                                         string? payload = "")
        {
            IsValid = isValid;
            Subject = subject;
            Payload = payload;
        }

        public bool IsValid { get; }

        public Guid Subject { get; }

        public string? Payload { get; }
    }
}
