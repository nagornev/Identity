using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Abstractions.Services;
using Notification.Backgrounds.Abstractions.Processors;

namespace Notification.Backgrounds.Processors
{
    public class OutboxBackgroundProcessor : IOutboxBackgroundProcessor
    {
        private readonly IServiceProvider _serviceProvider;

        public OutboxBackgroundProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellation = default)
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
