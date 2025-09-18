using MassTransit;
using MessageContracts;
using Notification.Application.Abstractions.Services;
using Notification.Messaging.Abstractions.Providers;

namespace Notification.Messaging.Consumers
{
    public class OneTimePasswordNotificationConsumer : IConsumer<OneTimePasswordNotificationMessageContract>
    {
        private readonly IChannelTypeProvider _channelTypeProvider;

        private readonly IOneTimePasswordNotificationMessageCreateService _oneTimePasswordNotificationMessageCreateService;

        public OneTimePasswordNotificationConsumer(IChannelTypeProvider channelTypeProvider,
                                                   IOneTimePasswordNotificationMessageCreateService oneTimePasswordNotificationMessageCreateService)
        {
            _channelTypeProvider = channelTypeProvider;
            _oneTimePasswordNotificationMessageCreateService = oneTimePasswordNotificationMessageCreateService;
        }

        public async Task Consume(ConsumeContext<OneTimePasswordNotificationMessageContract> context)
        {
            await _oneTimePasswordNotificationMessageCreateService.CreateAsync(context.Message.UserId,
                                                                               _channelTypeProvider.Get(context.Message.ChannelType),
                                                                               context.Message.ChannelValue,
                                                                               context.Message.OneTimePasswordValue,
                                                                               context.CancellationToken);
        }
    }
}
