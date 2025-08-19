namespace Auth.Application.Abstractions.Validators
{
    public interface IWindowValidator
    {
        bool Validate(long timestamp, long now, int window);
    }
}
