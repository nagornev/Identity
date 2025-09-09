namespace MessageContracts
{
    public class RolePermissionDeletedMessageContract
    {
        public RolePermissionDeletedMessageContract(Guid userId, Guid roleId, string name)
        {
            UserId = userId;
            RoleId = roleId;
            Name = name;
        }

        public Guid UserId { get; }

        public Guid RoleId { get; }

        public string Name { get; }
    }
}
