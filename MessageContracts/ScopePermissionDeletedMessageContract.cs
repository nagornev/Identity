namespace MessageContracts
{
    public record ScopePermissionDeletedMessageContract(Guid UserId, Guid ScopeId, string Audience, string Name)
    {
    }
}
