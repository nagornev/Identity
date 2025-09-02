using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContracts
{
    public class ScopePermissionDeletedMessageContract : IMessageContract
    {
        public ScopePermissionDeletedMessageContract(Guid userId, Guid scopeId, string audience, string name)
        {
            UserId = userId;
            ScopeId = scopeId;
            Audience = audience;
            Name = name;
        }

        public Guid UserId { get; }

        public Guid ScopeId { get; }

        public string Audience { get; }

        public string Name { get; }
    }
}
