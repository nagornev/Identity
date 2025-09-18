using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;

namespace Auth.Application.Services
{
    public class RefreshKeyDeletionService : KeyDeletionService<IRefreshKeyStorage>, IRefreshKeyDeletionService
    {
        public RefreshKeyDeletionService(IRefreshKeyStorage keyStorage, ITimeProvider timeProvider) : base(keyStorage, timeProvider)
        {
        }
    }
}
