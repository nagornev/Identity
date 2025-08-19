using Auth.Application.Abstractions.Services;

namespace Auth.Backgrounds.Processors
{
    public class KeyRotationBackgroundProcessor<TKeyRotationServiceType>
        where TKeyRotationServiceType : IKeyRotationService
    {
        private readonly IKeyRotationService _keyRotationService;

        public KeyRotationBackgroundProcessor(TKeyRotationServiceType keyRotationService)
        {
            _keyRotationService = keyRotationService;
        }

        public async Task HandleAsync(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                TimeSpan delay = await _keyRotationService.RotateAsync(cancellation);
                await Task.Delay(delay);
            }
        }
    }
}
