using Auth.Application.Abstractions.Services;
using Auth.Backgrounds.Abstractions.Processors;

namespace Auth.Backgrounds.Processors
{
    public class OutboxBackgroundProcessor : IOutboxBackroundProcessor
    {
        private readonly IOutboxService _outboxService;

        public OutboxBackgroundProcessor(IOutboxService outboxService)
        {
            _outboxService = outboxService;
        }

        public async Task HandleAsync(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                await _outboxService.HandleMessageAsync(cancellation);
            }
        }
    }
}
