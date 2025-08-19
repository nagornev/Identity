namespace Auth.Application.Abstractions.Providers
{
    public interface ITokenPayloadProvider<T>
    {
        T GetPayload(IReadOnlyDictionary<string, string> claims);
    }
}
