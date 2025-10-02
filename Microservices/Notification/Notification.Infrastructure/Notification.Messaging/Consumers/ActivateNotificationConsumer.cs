using MassTransit;
using MessageContracts;
using Notification.Application.Abstractions.Services;
using Notification.Messaging.Abstractions.Providers;

namespace Notification.Messaging.Consumers
{
    public class ActivateNotificationConsumer : IConsumer<ActivateNotificationMessageContract>
    {
        private readonly IChannelTypeProvider _channelTypeProvider;

        private readonly IActivateNotificationMessageCreateService _activateNotificationMessageCreateService;

        public ActivateNotificationConsumer(IChannelTypeProvider channelTypeProvider,
                                            IActivateNotificationMessageCreateService activateNotificationMessageCreateService)
        {
            _channelTypeProvider = channelTypeProvider;
            _activateNotificationMessageCreateService = activateNotificationMessageCreateService;
        }

        public async Task Consume(ConsumeContext<ActivateNotificationMessageContract> context)
        {
            await _activateNotificationMessageCreateService.CreateAsync(context.Message.UserId,
                                                                        _channelTypeProvider.Get(context.Message.ChannelType),
                                                                        context.Message.ChannelValue,
                                                                        context.Message.Url,
                                                                        context.CancellationToken);
        }
    }
}
