
using Auth.Backgrounds.Abstractions.Processors;

namespace Auth.Api.Backgrounds
{
    public class BackgroundsProcessorsStarter : BackgroundService
    {
        private readonly IEnumerable<IBackgroundProcessor> _backgroundProcessors;

        public BackgroundsProcessorsStarter(IEnumerable<IBackgroundProcessor> backgroundProcessors)
        {
            _backgroundProcessors = backgroundProcessors;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.WhenAll(_backgroundProcessors.Select(processor => processor.StartAsync(stoppingToken)));
        }
    }
}
