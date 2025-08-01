namespace Auth.Application.Abstractions.Services
{
    public interface IEmailAddressUpdateService
    {
        Task UpdateAsync(string emailToken, CancellationToken cancellation = default);
    }
}
