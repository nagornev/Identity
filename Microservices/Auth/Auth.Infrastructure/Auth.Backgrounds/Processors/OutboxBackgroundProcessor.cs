using Auth.Application.Abstractions.Services;
using Auth.Backgrounds.Abstractions.Processors;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Backgrounds.Processors
{
    public class OutboxBackgroundProcessor : IOutboxBackroundProcessor
    {
        private readonly IServiceProvider _serviceProvider;

        public OutboxBackgroundProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        IOutboxService outboxService = scope.ServiceProvider.GetRequiredService<IOutboxService>();
                        await outboxService.HandleAsync(cancellation);
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
