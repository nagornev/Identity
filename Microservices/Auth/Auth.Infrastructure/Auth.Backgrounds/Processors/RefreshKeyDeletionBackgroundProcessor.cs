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
    public class RefreshKeyDeletionBackgroundProcessor : KeyDeletionBackgroundProcessor<IRefreshKeyDeletionService, RefreshKeyOptions>, IRefreshKeyDeletionBackgroundProcessor
    {
        private const string _job = "delete-refresh-keys";

        public RefreshKeyDeletionBackgroundProcessor(IServiceProvider serviceProvider, IOptions<RefreshKeyOptions> keyOptions) 
            : base(_job, serviceProvider, keyOptions)
        {
        }
    }
}
