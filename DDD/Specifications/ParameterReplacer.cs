using System.Linq.Expressions;

namespace DDD.Specifications
{
    public class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _source;

        private readonly ParameterExpression _target;

        public ParameterReplacer(ParameterExpression source, ParameterExpression target)
        {
            _source = source;
            _target = target;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == _source ? _target : base.VisitParameter(node);
        }
    }
}
