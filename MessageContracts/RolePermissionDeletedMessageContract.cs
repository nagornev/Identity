namespace MessageContracts
{
    public record RolePermissionDeletedMessageContract(Guid UserId, Guid RoleId, string Name)
    {
    }
}
