using Auth.Backgrounds.Abstractions.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Backgrounds.Processors
{
    public class DeleteUnactivatedUsersBackgroundProcessors : IBackgroundProcessor
    {
        public Task HandleAsync(CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
