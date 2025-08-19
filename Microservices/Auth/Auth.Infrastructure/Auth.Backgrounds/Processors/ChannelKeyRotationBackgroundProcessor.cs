using Auth.Application.Abstractions.Services;
using Auth.Backgrounds.Abstractions.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Backgrounds.Processors
{
    public class ChannelKeyRotationBackgroundProcessor : KeyRotationBackgroundProcessor<IChannelKeyRotationService>, IChannelKeyRotationBackgroundProcessor
    {
        public ChannelKeyRotationBackgroundProcessor(IChannelKeyRotationService keyRotationService) 
            : base(keyRotationService)
        {
        }
    }
}
