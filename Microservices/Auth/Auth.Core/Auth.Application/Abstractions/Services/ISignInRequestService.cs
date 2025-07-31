namespace Auth.Application.Abstractions.Services
{
    public interface ISignInRequestService
    {
        Task<string> RequestAsync(string emailAddress, string password, CancellationToken cancellation = default);
    }
}
