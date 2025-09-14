namespace MessageContracts
{
    public record EmailAddressChangedMessageContract(Guid UserId, string EmailAddress)
    {
    }
}
