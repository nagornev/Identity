using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.DTOs
{
    public class OneTimePasswordCreation
    {
        public OneTimePasswordCreation(Guid oneTimePasswordId,
                                       string type,
                                       string channel,
                                       long expiresAt)
        {
            OneTimePasswordId = oneTimePasswordId;
            Type = type;
            Channel = channel;
            ExpiresAt = expiresAt;
        }

        public Guid OneTimePasswordId { get; }

        public string Type { get; }

        public string Channel { get; }

        public long ExpiresAt { get; }
    }
}
