using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Services
{
    public class ChannelKeyDeletionService : KeyDeletionService<IChannelKeyStorage>, IChannelKeyDeletionService
    {
        public ChannelKeyDeletionService(IChannelKeyStorage keyStorage, ITimeProvider timeProvider) 
            : base(keyStorage, timeProvider)
        {
        }
    }
}
