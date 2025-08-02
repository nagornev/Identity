namespace Auth.Application.Abstractions.Mappers
{
    public interface ITokenPayloadMapper<T>
    {
        Task<T> MapAsync(string token, CancellationToken cancellation = default);
    }
}
