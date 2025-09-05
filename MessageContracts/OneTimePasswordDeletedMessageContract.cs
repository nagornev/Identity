using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContracts
{
    public class OneTimePasswordDeletedMessageContract : IMessageContract
    {
        public OneTimePasswordDeletedMessageContract(Guid oneTimePasswordId, Guid subject, string tag, long createdAt, bool used)
        {
            OneTimePasswordId = oneTimePasswordId;
            Subject = subject;
            Tag = tag;
            CreatedAt = createdAt;
            Used = used;
        }

        public Guid OneTimePasswordId { get; }

        public Guid Subject { get; }

        public string Tag { get; }

        public long CreatedAt { get; }

        public bool Used { get; }
    }
}
