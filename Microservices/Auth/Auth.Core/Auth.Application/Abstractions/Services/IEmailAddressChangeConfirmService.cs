namespace Auth.Application.Abstractions.Services
{
    public interface IEmailAddressChangeConfirmService
    {
        Task ConfirmAsync(Guid otpId, string otp, CancellationToken cancellation = default);
    }
}
