using Microsoft.Extensions.DependencyInjection;
using Otp.Application.Abstractions.Services;
using Otp.Backgrounds.Abstractions.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Backgrounds.Processors
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
