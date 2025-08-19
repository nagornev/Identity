namespace Auth.Application.Abstractions.Providers
{
    public interface IFingerprintMessageProvider
    {
        string GetMessage(params object[] tokens);
    }
}
