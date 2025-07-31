namespace Auth.Application.Features.ChangePersonName
{
    public class PersonNameChangeCommand : ResultRequest
    {
        public PersonNameChangeCommand(Guid userId,
                                       string personName)
        {
            UserId = userId;
            PersonName = personName;
        }

        public Guid UserId { get; }

        public string PersonName { get; }
    }
}
