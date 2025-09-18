using Auth.Application.Abstractions.Clients;
using Auth.Application.Abstractions.Providers.Tokens;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Consts;
using Auth.Application.DTOs;

namespace Auth.Application.Services
{
    public class UserCreatedEventService : IUserCreatedEventService
    {
        private readonly IChannelTokenProvider _channelTokenProvider;

        private readonly IChannelKeyStorage _channelKeyStorage;

        private readonly INotificationClient notificationClient;

        public UserCreatedEventService(IChannelTokenProvider channelTokenProvider,
                                       IChannelKeyStorage channelKeyStorage,
                                       INotificationClient notificationClient)
        {
            _channelTokenProvider = channelTokenProvider;
            _channelKeyStorage = channelKeyStorage;
            this.notificationClient = notificationClient;
        }

        public async Task HandleAsync(Guid userId, string emailAddress, CancellationToken cancellation = default)
        {
            KeyPair channelPrimaryKey = await _channelKeyStorage.GetPrimaryAsync(cancellation);
            string channelToken = _channelTokenProvider.Create(new ChannelTokenCreationParameters(userId, ChannelTags.Email, emailAddress), channelPrimaryKey);

            await notificationClient.ActivateNotificationAsync(userId, emailAddress, channelToken);
        }
    }
}
