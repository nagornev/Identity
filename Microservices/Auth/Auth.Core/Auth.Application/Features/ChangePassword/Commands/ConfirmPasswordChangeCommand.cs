using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Features.ChangePassword.Commands
{
    public class ConfirmPasswordChangeCommand : ResultRequest
    {
        public ConfirmPasswordChangeCommand(string otpToken, string otp)
        {
            OtpToken = otpToken;
            Otp = otp;
        }

        public string OtpToken { get; }

        public string Otp { get; }
    }
}
