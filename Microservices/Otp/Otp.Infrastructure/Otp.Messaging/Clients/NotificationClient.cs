using MassTransit;
using MessageContracts;
using Otp.Application.Abstractions.Clients;
using Otp.Messaging.Abstractions.Providers;

namespace Otp.Messaging.Clients
{
    public class NotificationClient : INotificationClient
    {
        private readonly IPublishEndpoint _publishService;

        private readonly IChannelTypesProvider _channelTypesProvider;

        public NotificationClient(IPublishEndpoint publishService,
                                  IChannelTypesProvider channelTypesProvider)
        {
            _publishService = publishService;
            _channelTypesProvider = channelTypesProvider;
        }

        public async Task OneTimePasswordNotificationAsync(Guid userId, string oneTimePasswordValue, string type, string channel, CancellationToken cancellation = default)
        {
            OneTimePasswordNotificationMessageContract messageContract = new OneTimePasswordNotificationMessageContract(userId,
                                                                                                                        oneTimePasswordValue,
                                                                                                                        _channelTypesProvider.Get(type),
                                                                                                                        channel);

            await _publishService.Publish(messageContract, cancellation);
        }
    }
}
