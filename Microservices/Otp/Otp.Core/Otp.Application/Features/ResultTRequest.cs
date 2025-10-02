using MediatR;
using OperationResults;

namespace Otp.Application.Features
{
    public abstract class ResultTRequest<T> : IRequest<Result<T>>
    {
    }
}
