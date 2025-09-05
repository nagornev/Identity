namespace Auth.Application.Abstractions.Services
{
    public interface IPasswordChangeConfirmService
    {
        Task ConfirmAsync(Guid otpId, string otp, CancellationToken cancellation = default);
    }
}
