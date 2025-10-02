using OperationResults;

namespace Auth.Api.Providers
{
    public class ResultSuccessFactory
    {
        private Func<Result, IResult> _nocontentFactory;

        private IReadOnlyDictionary<Type, Func<object, IResult>> _contentFactory;

        public ResultSuccessFactory(Func<Result, IResult> nocontentFactory,
                                    IReadOnlyDictionary<Type, Func<object, IResult>> contentFactory)
        {
            _nocontentFactory = nocontentFactory;
            _contentFactory = contentFactory;
        }

        public IResult GetResult(Result result)
        {
            return _nocontentFactory.Invoke(result);
        }

        public IResult GetResult<TContentType>(Result<TContentType> result)
        {
            return _contentFactory.TryGetValue(typeof(Result<TContentType>), out var factory) ?
                        factory.Invoke(result) :
                        Results.Ok(result);
        }
    }
}
