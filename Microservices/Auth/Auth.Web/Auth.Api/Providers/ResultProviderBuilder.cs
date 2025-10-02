namespace Auth.Api.Providers
{
    public class ResultProviderBuilder
    {
        private ResultSuccessFactory _successFactory;

        private ResultFailedFactory _failedFactory;

        public ResultProviderBuilder UseSuccess(Action<ResultSuccessFactoryBuilder> options)
        {
            ResultSuccessFactoryBuilder builder = new ResultSuccessFactoryBuilder();

            options.Invoke(builder);

            _successFactory = builder.Build();

            return this;
        }

        public ResultProviderBuilder UseFailed(Action<ResultFailedFactoryBuilder> options)
        {
            ResultFailedFactoryBuilder builder = new ResultFailedFactoryBuilder();

            options.Invoke(builder);

            _failedFactory = builder.Build();

            return this;
        }


        internal ResultProvider Build()
        {
            if (_successFactory == null ||
               _failedFactory == null)
                throw new ArgumentNullException(string.Empty, "The success or failed factories can not be null.");

            return new ResultProvider(_successFactory, _failedFactory);
        }
    }
}
