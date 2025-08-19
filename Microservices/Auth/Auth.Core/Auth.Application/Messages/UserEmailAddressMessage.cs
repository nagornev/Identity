namespace Auth.Application.Messages
{
    public class UserEmailAddressMessage
    {
        public UserEmailAddressMessage(string emailAddress, string message)
        {
            EmailAddress = emailAddress;
            Message = message;
        }

        public string EmailAddress { get; }

        public string Message { get; }
    }
}
