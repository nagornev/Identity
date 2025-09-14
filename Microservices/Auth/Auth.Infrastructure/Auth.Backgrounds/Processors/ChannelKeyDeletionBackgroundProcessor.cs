using Auth.Application.Abstractions.Services;
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
    public class ChannelKeyDeletionBackgroundProcessor : KeyDeletionBackgroundProcessor<IChannelKeyDeletionService, ChannelKeyOptions>, IChannelKeyDeletionBackgroundProcessor
    {
        private const string _job = "delete-channel-keys";

        public ChannelKeyDeletionBackgroundProcessor(IServiceProvider serviceProvider, IOptions<ChannelKeyOptions> keyOptions) 
            : base(_job, serviceProvider, keyOptions)
        {
        }
    }
}
