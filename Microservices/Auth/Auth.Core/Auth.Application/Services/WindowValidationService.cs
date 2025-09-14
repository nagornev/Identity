using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Validators;
using Auth.Application.Exceptions.Applications.Security;

namespace Auth.Application.Services
{
    public class WindowValidationService : IWindowValidationService
    {
        private readonly IWindowValidator _windowValidator;

        private readonly ITimeProvider _timeProvider;

        public WindowValidationService(IWindowValidator windowValidator,
                                       ITimeProvider timeProvider)
        {
            _windowValidator = windowValidator;
            _timeProvider = timeProvider;
        }

        public void Validate(long timestamp, int window)
        {
            if (!_windowValidator.Validate(timestamp, _timeProvider.NowUnix(), window))
                throw new WindowInvalidApplicationException();
        }
    }
}
