namespace Auth.Application.Abstractions.Validators.Tokens
{
    public interface ITokenValidator
    {
        Task<bool> ValidateAsync(string token, CancellationToken cancellation = default);
    }
}
