using Auth.Application.Abstractions.Services;
using Auth.Backgrounds.Abstractions.Processors;

namespace Auth.Backgrounds.Processors
{
    public class DeleteUnactivatedUsersBackgroundProcessors : IBackgroundProcessor
    {
        private readonly IDeleteUnactivatedUsersBackgroundService _deleteUnactivatedUsersBackgroundService;

        public DeleteUnactivatedUsersBackgroundProcessors(IDeleteUnactivatedUsersBackgroundService deleteUnactivatedUsersBackgroundService)
        {
            _deleteUnactivatedUsersBackgroundService = deleteUnactivatedUsersBackgroundService;
        }

        public async Task HandleAsync(CancellationToken cancellation)
        {
            await _deleteUnactivatedUsersBackgroundService.HandleAsync(cancellation);
        }
    }
}
