using Microsoft.Extensions.Logging;
using Notification.Application.Abstractions.Senders;
using Notification.Domain.Consts;

namespace Notification.Application.Senders
{
    public class SmsMessageSender : IMessageSender
    {
        private readonly ILogger<SmsMessageSender> _logger;

        public SmsMessageSender(ILogger<SmsMessageSender> logger)
        {
            _logger = logger;
        }

        public string GetHandableType()
        {
            return ChannelType.Sms;
        }

        public Task SendAsync(string channel, string text, CancellationToken cancellation = default)
        {
            //TODO: any sms service
            _logger.LogInformation("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n" +
                                   $"Channel - {channel}\n" +
                                   $"Text: {text}\n" +
                                   $"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");

            return Task.CompletedTask;
        }
    }
}
