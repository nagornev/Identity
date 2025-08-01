namespace Auth.Application.Abstractions.Services
{
    public interface IPasswordChangeRequestService
    {
        Task<string> RequestAsync(Guid userId, string oldPassword, string newPassword, CancellationToken cancellation = default);
    }
}
