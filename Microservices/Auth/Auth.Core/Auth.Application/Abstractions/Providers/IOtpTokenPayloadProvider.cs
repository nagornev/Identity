namespace Auth.Application.Abstractions.Providers
{
    public interface IOtpTokenPayloadProvider
    {
        string Serialize<T>(T obj);

        T Deserialize<T>(string payload);
    }
}
