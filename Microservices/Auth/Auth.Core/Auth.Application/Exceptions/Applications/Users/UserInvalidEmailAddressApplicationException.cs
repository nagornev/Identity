namespace Auth.Application.Exceptions.Applications.Users
{
    public class UserInvalidEmailAddressApplicationException : AlreadyApplicationException
    {
        public UserInvalidEmailAddressApplicationException(string emailAddress)
            : base($"The email ({emailAddress}) is invalid for this user.")
        {
        }
    }
}
