using MediatR;
using OperationResults;

namespace Otp.Application.Features
{
    public abstract class ResultRequest : IRequest<Result>
    {
    }
}
