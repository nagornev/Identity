using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.DTOs
{
    public class EmailTokenCreationParameters
    {
        public EmailTokenCreationParameters(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
