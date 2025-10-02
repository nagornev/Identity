namespace Auth.Application.Abstractions.Services
{
    public interface ISignUpRequestService
    {
        Task RequestAsync(string emailAddress, string personName, string password, CancellationToken cancellation = default);
    }
}
