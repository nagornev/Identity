namespace Auth.Application.Abstractions.Services
{
    public interface IPasswordChangeRequestService
    {
        Task<Guid> RequestAsync(Guid userId, string oldPassword, string newPassword, CancellationToken cancellation = default);
    }
}
