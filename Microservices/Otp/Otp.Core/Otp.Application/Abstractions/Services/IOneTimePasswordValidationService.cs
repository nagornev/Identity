using Otp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Abstractions.Services
{
    public interface IOneTimePasswordValidationService
    {
        Task<OneTimePasswordValidation> ValidateAsync(Guid oneTimePasswordId, string oneTimePasswordValue, string tag,  CancellationToken cancellation = default);
    }
}
