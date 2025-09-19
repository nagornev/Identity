namespace MessageContracts
{
    public record LogMessageContract(string Type, string Message, string? StackTrace, long CreatedAt)
    {
    }
}
