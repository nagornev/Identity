namespace Auth.Domain.Exceptions.Domains.Users
{
    public class PersonNameLengthOutOfRangeDomainException : OutOfRangeDomainException
    {
        private const string _message = "The person name cannot be less {0} and more {1} symbols.";

        public PersonNameLengthOutOfRangeDomainException(int minimum, int maximum)
            : base(string.Format(_message, minimum, maximum))
        {
        }
    }
}
