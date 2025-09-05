namespace Auth.Application.Features.ChangeEmailAddress.Commands
{
    public class RequestEmailAddressChangeCommand : ResultTRequest<Guid>
    {
        public RequestEmailAddressChangeCommand(Guid userId,
                                                string emailAddress)
        {
            UserId = userId;
            EmailAddress = emailAddress;
        }

        public Guid UserId { get; }

        public string EmailAddress { get; }
    }
}
