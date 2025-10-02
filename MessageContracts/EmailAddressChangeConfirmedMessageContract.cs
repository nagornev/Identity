namespace MessageContracts
{
    public record EmailAddressChangeConfirmedMessageContract(Guid UserId, string NewEmailAddress)
    {
    }
}
