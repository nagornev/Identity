using MassTransit;
using MessageContracts;
using Otp.Application.Abstractions.Clients;

namespace Otp.Messaging.Clients
{
    public class NotificationClient : INotificationClient
    {
        private readonly IPublishEndpoint _publishService;

        public NotificationClient(IPublishEndpoint publishService)
        {
            _publishService = publishService;
        }

        public async Task OneTimePasswordNotificationAsync(Guid subject, string oneTimePasswordValue, CancellationToken cancellation = default)
        {
            OneTimePasswordNotificationMessageContract messageContract = new OneTimePasswordNotificationMessageContract(subject, oneTimePasswordValue);

            await _publishService.Publish(messageContract, cancellation);
        }
    }
}
