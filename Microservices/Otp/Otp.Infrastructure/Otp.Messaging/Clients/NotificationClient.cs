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

        public async Task OneTimePasswordNotificationAsync(Guid subject, string oneTimePasswordValue, CancellationToken cancellation = default)
        {
            OneTimePasswordNotificationMessageContract messageContract = new OneTimePasswordNotificationMessageContract(subject, oneTimePasswordValue);

            _logger.LogInformation($"{oneTimePasswordValue}____________________________________________________CODE!!!!");

            //await _publishService.Publish(messageContract, cancellation);
        }
    }
}
