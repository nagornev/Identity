using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Abstractions.Validators
{
    public interface IRefreshSignatureValidator
    {
        Task<bool> ValidateAsync(string refreshToken, string newPublicKey, long timestamp, string signature, CancellationToken cancellation = default);
    }
}
