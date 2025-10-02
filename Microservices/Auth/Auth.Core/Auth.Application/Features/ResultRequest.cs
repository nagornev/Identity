using MediatR;
using OperationResults;

namespace Auth.Application.Features
{
    public abstract class ResultRequest : IRequest<Result>
    {
    }
}
