using MassTransit;
using MessageContracts;
using Otp.Application.Abstractions.Providers;
using Otp.Application.Abstractions.Services;

namespace Otp.Messaging.Services
{
    public class LogService : ILogService
    {
        private const string _authLogQueueName = "otp-log-queue";

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
