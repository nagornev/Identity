namespace Auth.Application.Abstractions.Mappers
{
    public interface ITokenMapper<T>
    {
        Task<T> MapAsync(string token, CancellationToken cancellation = default);
    }
}
