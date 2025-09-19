using OperationResults;

namespace Auth.Api.Providers
{
    public class ResultFailedFactoryBuilder
    {
        private Func<Result, IResult> _factory;

        public ResultFailedFactoryBuilder UseFactory(Func<Result, IResult> factory)
        {
            _factory = factory;

            return this;
        }

        internal ResultFailedFactory Build()
        {
            if (_factory == null)
                throw new ArgumentNullException(string.Empty, "The failed result factory can not be null.");

            return new ResultFailedFactory(_factory);
        }
    }
}
