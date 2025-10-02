using Microsoft.Extensions.Logging;
using Notification.Application.Abstractions.Senders;
using Notification.Domain.Consts;

namespace Notification.Application.Senders
{
    public class EmailMessageSender : IEmailMessageSender
    {
        private readonly ILogger<EmailMessageSender> _logger;

        public EmailMessageSender(ILogger<EmailMessageSender> logger)
        {
            _logger = logger;
        }

        public string GetHandableType()
        {
            return ChannelType.Email;
        }

        public Task SendAsync(string channel, string text, CancellationToken cancellation = default)
        {
            //TODO: SMTP

            _logger.LogInformation("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n" +
                                   $"Channel - {channel}\n" +
                                   $"Text: {text}\n" +
                                   $"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");

            return Task.CompletedTask;
        }
    }
}
