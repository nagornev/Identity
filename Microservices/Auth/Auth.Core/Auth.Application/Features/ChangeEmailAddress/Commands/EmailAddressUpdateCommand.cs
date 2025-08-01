using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Features.ChangeEmailAddress.Commands
{
    public class EmailAddressUpdateCommand:ResultRequest
    {
        public EmailAddressUpdateCommand(string emailToken)
        {
            EmailToken = emailToken;
        }

        public string EmailToken { get; }
    }
}
