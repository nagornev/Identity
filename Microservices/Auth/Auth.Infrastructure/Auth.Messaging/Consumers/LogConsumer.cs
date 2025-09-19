using MassTransit;
using MessageContracts;
using Microsoft.Extensions.Logging;

namespace Auth.Messaging.Consumers
{
    public class LogConsumer : IConsumer<LogMessageContract>
    {
        private readonly ILogger<LogConsumer> _logger;

        public LogConsumer(ILogger<LogConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<LogMessageContract> context)
        {
            _logger.LogError("Exception: {@Error}", context.Message);

            return Task.CompletedTask;
        }
    }
}
