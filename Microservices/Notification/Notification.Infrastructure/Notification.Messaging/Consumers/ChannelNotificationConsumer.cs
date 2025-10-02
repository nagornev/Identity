using MassTransit;
using MessageContracts;
using Notification.Application.Abstractions.Services;
using Notification.Messaging.Abstractions.Providers;

namespace Notification.Messaging.Consumers
{
    public class ChannelNotificationConsumer : IConsumer<ChannelNotificationMessageContract>
    {
        private readonly IChannelTypeProvider _channelTypeProvider;

        private readonly IChannelNotificationMessageCreateService _channelNotificationMessageCreateService;

        public ChannelNotificationConsumer(IChannelTypeProvider channelTypeProvider,
                                           IChannelNotificationMessageCreateService channelNotificationMessageCreateService)
        {
            _channelTypeProvider = channelTypeProvider;
            _channelNotificationMessageCreateService = channelNotificationMessageCreateService;
        }

        public async Task Consume(ConsumeContext<ChannelNotificationMessageContract> context)
        {
            await _channelNotificationMessageCreateService.CreateAsync(context.Message.UserId,
                                                                       _channelTypeProvider.Get(context.Message.ChannelType),
                                                                       context.Message.ChannelValue,
                                                                       context.Message.Url,
                                                                       context.CancellationToken);
        }
    }
}
