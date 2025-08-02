using Auth.Domain.Aggregates;
using DDD.Specifications;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications
{
    public class RoleByIdsSpecification : Specification<Role>
    {
        private readonly IEnumerable<Guid> _ids;

        public RoleByIdsSpecification(IEnumerable<Guid> ids)
        {
            _ids = ids;
        }

        public override Expression<Func<Role, bool>> ToExpression()
        {
            return x => _ids.Contains(x.Id);
        }
    }
}
