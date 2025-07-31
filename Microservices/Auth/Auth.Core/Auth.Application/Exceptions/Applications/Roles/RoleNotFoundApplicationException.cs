namespace Auth.Application.Exceptions.Applications.Roles
{
    public class RoleNotFoundApplicationException : NotFoundApplicationException
    {
        public RoleNotFoundApplicationException(Guid id)
            : base(string.Format($"The role with this ID ({id}) was not found."))
        {
        }

        public RoleNotFoundApplicationException(string name)
            : base(string.Format($"The role with this name ({name}) was not found."))
        {
        }
    }
}
