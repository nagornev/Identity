using Otp.Application.Abstractions.Clients;
using Otp.Application.Abstractions.Services;
using Otp.Domain.Aggregates;

namespace Otp.Application.Services
{
    public class OneTimePasswordCreatedEventService : IOneTimePasswordCreatedEventService
    {
        private readonly IOneTimePasswordQueryService _oneTimePasswordQueryService;

        private readonly INotificationClient _notificationClient;

        public OneTimePasswordCreatedEventService(IOneTimePasswordQueryService oneTimePasswordQueryService,
                                                  INotificationClient notificationClient)
        {
            _oneTimePasswordQueryService = oneTimePasswordQueryService;
            _notificationClient = notificationClient;
        }

        public async Task HandleAsync(Guid oneTimePasswordId, CancellationToken cancellation = default)
        {
            OneTimePassword oneTimePassword = await _oneTimePasswordQueryService.GetOneTimePasswordByIdAsync(oneTimePasswordId, cancellation);

            await _notificationClient.OneTimePasswordNotificationAsync(oneTimePassword.UserId,
                                                                       oneTimePassword.GetValue(),
                                                                       oneTimePassword.Channel.Type,
                                                                       oneTimePassword.Channel.Value);
        }
    }
}
