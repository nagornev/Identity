using MediatR;
using Results;

namespace Auth.Application.Features
{
    public abstract class ResultRequest : IRequest<Result>
    {
    }
}
