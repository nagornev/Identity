namespace Auth.Application.DTOs
{
    public class FingerprintValidationParameters
    {
        public FingerprintValidationParameters(string message,
                                               string signature)
        {
            Message = message;
            Signature = signature;
        }

        public string Message { get; }

        public string Signature { get; }
    }
}
