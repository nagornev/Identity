namespace Auth.Application.Abstractions.Validators
{
    public interface IEmailTokenValidator
    {
        Task<bool> ValidateAsync(string token, CancellationToken cancellation = default);
    }
}
