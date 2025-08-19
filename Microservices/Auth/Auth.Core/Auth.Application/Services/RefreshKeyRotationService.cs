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
    public class RefreshKeyRotationService : KeyRotationService<RefreshKeyOptions, IRefreshKeyStorage, IRefreshKeyPairFactory>, IRefreshKeyRotationService
    {
        public RefreshKeyRotationService(IRefreshKeyStorage keysStorage,
                                         IRefreshKeyPairFactory keysFactory,
                                         ITimeProvider timeProvider,
                                         IOptions<RefreshKeyOptions> keyOptions) 
            : base(keysStorage, keysFactory, timeProvider, keyOptions)
        {
        }
    }
}
