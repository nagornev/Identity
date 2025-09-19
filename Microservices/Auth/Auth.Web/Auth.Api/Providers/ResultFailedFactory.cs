using OperationResults;

namespace Auth.Api.Providers
{
    public class ResultFailedFactory
    {
        private Func<Result, IResult> _factory;

        public ResultFailedFactory(Func<Result, IResult> factory)
        {
            _factory = factory;
        }

        public IResult GetResult(Result result)
        {
            return _factory.Invoke(result);
        }
    }
}
