namespace MessageContracts
{
    public class OtpCreationCompleted : IMessageContract
    {
        public OtpCreationCompleted(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}
