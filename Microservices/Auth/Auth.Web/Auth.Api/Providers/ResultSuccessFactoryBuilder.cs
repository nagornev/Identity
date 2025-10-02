using OperationResults;

namespace Auth.Api.Providers
{
    public class ResultSuccessFactoryBuilder
    {
        private Func<Result, IResult> _nocontentFactory;

        private Dictionary<Type, Func<object, IResult>> _contentFactory;

        public ResultSuccessFactoryBuilder()
        {
            _contentFactory = new Dictionary<Type, Func<object, IResult>>();
        }

        public ResultSuccessFactoryBuilder UseNocontentFactory(Func<Result, IResult> nocontentFactory)
        {
            _nocontentFactory = nocontentFactory;

            return this;
        }

        public ResultSuccessFactoryBuilder AddContentFactory<TContentType>(Func<Result<TContentType>, IResult> contentFactory)
        {
            _contentFactory.Add(typeof(Result<TContentType>), (obj) => contentFactory.Invoke((Result<TContentType>)obj));

            return this;
        }

        internal ResultSuccessFactory Build()
        {
            if (_nocontentFactory == null)
                throw new ArgumentNullException(string.Empty, "The success nocontent factory can not be null.");

            return new ResultSuccessFactory(_nocontentFactory, _contentFactory);
        }

    }
}
