using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;
using Auth.Application.Options;
using Auth.Backgrounds.Abstractions.Processors;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
