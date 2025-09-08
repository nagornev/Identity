namespace Auth.Application.Abstractions.Services
{
    public interface IEmailAddressChangeConfirmedEventService
    {
        Task HandleAsync(Guid userId, string emailAddress, CancellationToken cancellation = default);
    }
}
