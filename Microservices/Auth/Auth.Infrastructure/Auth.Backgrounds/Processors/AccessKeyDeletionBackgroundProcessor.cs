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
    public class AccessKeyDeletionBackgroundProcessor : KeyDeletionBackgroundProcessor<IAccessKeyDeletionService, AccessKeyOptions>, IAccessKeyDeletionBackgroundProcessor
    {
        private const string _job = "delete-access-keys";

        public AccessKeyDeletionBackgroundProcessor(IServiceProvider serviceProvider, IOptions<AccessKeyOptions> keyOptions) 
            : base(_job, serviceProvider, keyOptions)
        {
        }
    }
}
