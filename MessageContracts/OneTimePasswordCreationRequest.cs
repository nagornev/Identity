using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContracts
{
    public class OneTimePasswordCreationRequest : IRequestMessageContract
    {
        public OneTimePasswordCreationRequest(Guid subject, string tag, string payload)
        {
            Subject = subject;
            Tag = tag;
            Payload = payload;
        }

        public Guid Subject { get; }

        public string Tag { get; }

        public string Payload { get; }
    }
}
