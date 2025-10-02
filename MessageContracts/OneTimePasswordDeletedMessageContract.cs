namespace MessageContracts
{
    public record OneTimePasswordDeletedMessageContract(Guid OneTimePasswordId, Guid Userid, string Tag, long CreatedAt, bool Used)
    {
    }
}
