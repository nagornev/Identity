namespace Auth.Application.Abstractions.Services
{
    public interface IPersonNameService
    {
        Task ChangeAsync(Guid userId, string personName, CancellationToken cancellation = default);
    }
}
