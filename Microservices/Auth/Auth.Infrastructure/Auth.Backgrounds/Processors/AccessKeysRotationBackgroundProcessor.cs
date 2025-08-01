using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Options;
using Auth.Backgrounds.Abstractions.Processors;
using Microsoft.Extensions.Options;

namespace Auth.Backgrounds.Processors
{
    public class AccessKeysRotationBackgroundProcessor : KeysRotationBackgroundProcessor<AccessKeyOptions, IAccessKeyStorage, IAccessKeyFactory>, IAccessKeysRotationBackroundProcessor
    {
        public AccessKeysRotationBackgroundProcessor(IAccessKeyStorage keysStorage,
                                                     IAccessKeyFactory keysFactory,
                                                     IOptions<AccessKeyOptions> keyOptions,
                                                     ITimeProvider timeProvider) : base(keysStorage, keysFactory, keyOptions, timeProvider)
        {
        }
    }
}
