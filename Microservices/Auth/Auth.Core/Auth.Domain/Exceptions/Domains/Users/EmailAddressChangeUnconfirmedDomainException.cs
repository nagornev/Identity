namespace Auth.Domain.Exceptions.Domains.Users
{
    public class EmailAddressChangeUnconfirmedDomainException : UnconfirmedDomainException
    {
        private const string _message = "The user`s email address change is unconfirmed.";

        public EmailAddressChangeUnconfirmedDomainException()
            : base(_message)
        {
        }
    }
}
