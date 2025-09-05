namespace Auth.Application.Abstractions.Validators
{
    public interface IPasswordValidator
    {
        bool Verify(string password, string hash, string salt);
    }
}
