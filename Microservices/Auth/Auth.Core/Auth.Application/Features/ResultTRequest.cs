using MediatR;
using OperationResults;

namespace Auth.Application.Features
{
    public abstract class ResultTRequest<T> : IRequest<Result<T>>
    {
    }
}
