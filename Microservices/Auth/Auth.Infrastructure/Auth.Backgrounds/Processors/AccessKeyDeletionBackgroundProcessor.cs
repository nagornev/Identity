using Auth.Application.Abstractions.Services;
using Auth.Application.Options;
using Auth.Backgrounds.Abstractions.Processors;
using Microsoft.Extensions.Options;

namespace Auth.Backgrounds.Processors
{
    public class AccessKeyDeletionBackgroundProcessor : KeyDeletionBackgroundProcessor<IAccessKeyDeletionService, AccessKeyOptions>, IAccessKeyDeletionBackgroundProcessor
    {
        private const string _job = "delete-access-keys";

        public AccessKeyDeletionBackgroundProcessor(IServiceProvider serviceProvider, IOptions<AccessKeyOptions> keyOptions)
            : base(_job, serviceProvider, keyOptions)
        {
        }
    }
}
