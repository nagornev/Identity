using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;

namespace Auth.Application.Services
{
    public class AccessKeyDeletionService : KeyDeletionService<IAccessKeyStorage>, IAccessKeyDeletionService
    {
        public AccessKeyDeletionService(IAccessKeyStorage keyStorage, ITimeProvider timeProvider)
            : base(keyStorage, timeProvider)
        {
        }
    }
}
