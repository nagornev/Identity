namespace Auth.Application.Abstractions.Services
{
    public interface ISignUpConfirmService
    {
        Task ConfirmAsync(string emailToken, CancellationToken cancellation = default);
    }
}
