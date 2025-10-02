using Auth.Application.Abstractions.Clients;
using Auth.Application.Abstractions.Providers.Tokens;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Consts;
using Auth.Application.DTOs;

namespace Auth.Application.Services
{
    public class EmailAddressChangeConfirmedEventService : IEmailAddressChangeConfirmedEventService
    {
        private readonly IChannelKeyStorage _channelKeyStorage;

        private readonly IChannelTokenProvider _channelTokenProvider;

        private readonly INotificationClient _notificationClient;

        public EmailAddressChangeConfirmedEventService(IChannelKeyStorage channelKeyStorage,
                                                       IChannelTokenProvider channelTokenProvider,
                                                       INotificationClient notificationClient)
        {
            _channelKeyStorage = channelKeyStorage;
            _channelTokenProvider = channelTokenProvider;
            _notificationClient = notificationClient;
        }

        public async Task HandleAsync(Guid userId, string emailAddress, CancellationToken cancellation = default)
        {
            KeyPair channelPrimaryKey = await _channelKeyStorage.GetPrimaryAsync(cancellation);
            string token = _channelTokenProvider.Create(new ChannelTokenCreationParameters(userId, ChannelTags.Email, emailAddress), channelPrimaryKey);

            await _notificationClient.EmailChannelNotificationAsync(userId, emailAddress, token);
        }
    }
}
