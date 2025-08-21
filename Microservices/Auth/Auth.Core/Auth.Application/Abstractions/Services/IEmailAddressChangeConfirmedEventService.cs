using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Abstractions.Services
{
    public interface IEmailAddressChangeConfirmedEventService
    {
        Task HandleAsync(Guid userId, string emailAddress, CancellationToken cancellation = default);
    }
}
