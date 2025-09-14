namespace MessageContracts
{
    public record UserCreatedMessageContract(Guid UserId, string EmailAddress)
    {
    }
}
