using Auth.Domain.Aggregates;
using DDD.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications
{
    public class ScopeByIdSpecification : Specification<Scope>
    {
        private readonly Guid _id;

        public ScopeByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<Scope, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}
