namespace Otp.Application.Exceptions.Applications.OneTimePasswords
{
    public class OneTimePasswordNotFoundApplicationException : NotFoundApplicationException
    {
        public OneTimePasswordNotFoundApplicationException(Guid id)
            : base($"The one time password with id ({id}) was not found.")
        {
        }
    }
}
