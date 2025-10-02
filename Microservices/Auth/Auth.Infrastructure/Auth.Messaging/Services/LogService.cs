using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using MassTransit;
using MessageContracts;

namespace Auth.Messaging.Services
{
    public class LogService : ILogService
    {
        private const string _authLogQueueName = "auth-log-queue";

        private readonly IBus _bus;

        private readonly ITimeProvider _timeProvider;

        public LogService(IBus bus,
                          ITimeProvider timeProvider)
        {
            _bus = bus;
            _timeProvider = timeProvider;
        }

        public async Task LogError(Exception exception, CancellationToken cancellation = default)
        {
            LogMessageContract log = new LogMessageContract(exception.GetType().Name, exception.Message, exception.StackTrace, _timeProvider.NowUnix());

            ISendEndpoint sendEndpoint = await _bus.GetSendEndpoint(new Uri($"queue:{_authLogQueueName}"));
            await sendEndpoint.Send(log, cancellation);
        }
    }
}
