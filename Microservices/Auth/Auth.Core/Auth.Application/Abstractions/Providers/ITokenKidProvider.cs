namespace Auth.Application.Abstractions.Providers
{
    public interface ITokenKidProvider
    {
        Guid? Get(string token);
    }
}
