using MassTransit;
using MessageContracts;
using Microsoft.Extensions.Logging;
using Otp.Application.Abstractions.Clients;

namespace Otp.Messaging.Clients
{
    public class NotificationClient : INotificationClient
    {
        private readonly IPublishEndpoint _publishService;

        private readonly ILogger<NotificationClient> _logger;

        public NotificationClient(IPublishEndpoint publishService,
                                  ILogger<NotificationClient> logger)
        {
            _publishService = publishService;
            _logger = logger;
        }

        public async Task OneTimePasswordNotificationAsync(Guid userId, string oneTimePasswordValue, string type, string channel, CancellationToken cancellation = default)
        {
            OneTimePasswordNotificationMessageContract messageContract = new OneTimePasswordNotificationMessageContract(userId, oneTimePasswordValue, type, channel);

            _logger.LogInformation($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n"+
                                   $"Code - {oneTimePasswordValue}.\n" +
                                   $"Type - {type}, channel - {channel}.\n" +
                                   $"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");

            //await _publishService.Publish(messageContract, cancellation);
        }
    }
}
