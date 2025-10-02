namespace Otp.Application.Exceptions.Infrastructures.Messaging
{
    public class MessagingInvalidOperationInfrastructureException : InvalidInfrastructureException
    {
        public MessagingInvalidOperationInfrastructureException(string message, Exception? inner = null)
            : base(message, inner)
        {
        }
    }
}
