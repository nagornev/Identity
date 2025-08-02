using Auth.Domain.Aggregates;
using DDD.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications
{
    public class ScopeByIdsSpecification : Specification<Scope>
    {
        private readonly IEnumerable<Guid> _ids;

        public ScopeByIdsSpecification(IEnumerable<Guid> ids)
        {
            _ids = ids;
        }

        public override Expression<Func<Scope, bool>> ToExpression()
        {
            return x => _ids.Contains(x.Id);
        }
    }
}
