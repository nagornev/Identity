namespace MessageContracts
{
    public record UserActivatedMessageContract(Guid UserId, string EmailAddress)
    {
    }
}
