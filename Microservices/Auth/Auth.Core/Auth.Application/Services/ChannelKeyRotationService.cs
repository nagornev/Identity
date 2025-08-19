using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Services
{
    public class ChannelKeyRotationService : KeyRotationService<ChannelKeyOptions, IChannelKeyStorage, IChannelKeyPairFactory>, IChannelKeyRotationService
    {
        public ChannelKeyRotationService(IChannelKeyStorage keysStorage,
                                         IChannelKeyPairFactory keysFactory,
                                         ITimeProvider timeProvider,
                                         IOptions<ChannelKeyOptions> keyOptions) 
            : base(keysStorage, keysFactory, timeProvider, keyOptions)
        {
        }
    }
}
