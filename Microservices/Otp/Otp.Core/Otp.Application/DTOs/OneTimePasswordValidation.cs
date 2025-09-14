namespace Otp.Application.DTOs
{
    public class OneTimePasswordValidation
    {
        public OneTimePasswordValidation(bool isValid,
                                         Guid userId,
                                         string? payload = "")
        {
            IsValid = isValid;
            UserId = userId;
            Payload = payload;
        }

        public bool IsValid { get; }

        public Guid UserId { get; }

        public string? Payload { get; }
    }
}
