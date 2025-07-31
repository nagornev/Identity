using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Abstractions.Validators
{
    public interface IRefreshTokenValidator
    {
        Task<bool> ValidateAsync(string refreshToken, CancellationToken cancellation);
    }
}
