namespace Auth.Application.Abstractions.Services
{
    public interface IWindowValidationService
    {
        void Validate(long timestamp, int window);
    }
}
