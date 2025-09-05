namespace Auth.Application.Abstractions.Services
{
    public interface IEmailAddressChangeRequestService
    {
        Task<Guid> RequestAsync(Guid userId, string emailAddress, CancellationToken cancellation);
    }
}
