using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.DTOs
{
    public class ChangePasswordHashOtpTokenPayload
    {
        public ChangePasswordHashOtpTokenPayload(Guid version)
        {
            Version = version;
        }

        public Guid Version { get; }
    }
}
