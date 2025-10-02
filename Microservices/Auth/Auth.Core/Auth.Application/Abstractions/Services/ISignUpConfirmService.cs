namespace Auth.Application.Abstractions.Services
{
    public interface ISignUpConfirmService
    {
        Task ConfirmAsync(string channelToken, CancellationToken cancellation = default);
    }
}
