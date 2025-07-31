namespace Auth.Application.Abstractions.Services
{
    public interface IEmailAddressChangeRequestService
    {
        Task<string> RequestAsync(Guid userId, string emailAddress, CancellationToken cancellation);
    }
}
