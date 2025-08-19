using Auth.Application.Abstractions.Validators;

namespace Auth.Application.Validators
{
    public class WindowValidator : IWindowValidator
    {
        public bool Validate(long timestamp, long now, int window)
        {
            return now - timestamp < window;
        }
    }
}
