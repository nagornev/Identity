using MessageContracts;

namespace Auth.Application.Abstractions.Services
{
    public interface IPublishMessageService
    {
        Task PublishAsync<T>(T message, CancellationToken cancellation = default)
            where T : IMessageContract;
    }
}
