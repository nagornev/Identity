using MediatR;
using Results;

namespace Auth.Application.Features
{
    public abstract class ResultTRequest<T> : IRequest<Result<T>>
    {
    }
}
