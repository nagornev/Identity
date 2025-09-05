using Otp.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Abstractions.Services
{
    public interface IOneTimeCreateService
    {
        Task<Guid> CreateAsync(string tag, Guid subject, string? payload = "", CancellationToken cancellation =default);
    }
}
