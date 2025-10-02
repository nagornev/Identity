using OperationResults;

namespace Auth.Api.Abstractions.Providers
{
    public interface IResultProvider
    {
        IResult GetResult(Result result);

        IResult GetResult<T>(Result<T> result);
    }
}
