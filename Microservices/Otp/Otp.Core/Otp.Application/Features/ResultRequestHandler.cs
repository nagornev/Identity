using MediatR;
using OperationResults;
using Otp.Application.Exceptions.Infrastructures;
using Otp.Domain.Exceptions.Domains;
using ApplicationException = Otp.Application.Exceptions.Applications.ApplicationException;

namespace Otp.Application.Features
{
    public abstract class ResultRequestHandler<TRequestType> : IRequestHandler<TRequestType, Result>
        where TRequestType : ResultRequest
    {
        public async Task<Result> Handle(TRequestType request, CancellationToken cancellationToken)
        {
            try
            {
                await HandleAsync(request, cancellationToken);

                return Result.Success();
            }
            catch (DomainException domainException)
            {
                return domainException.GetResult();
            }
            catch (ApplicationException applicationException)
            {
                return applicationException.GetResult();
            }
            catch (InfrastructureException infastructureException)
            {
                return infastructureException.GetResult();
            }
        }

        public abstract Task HandleAsync(TRequestType request, CancellationToken cancellation);

    }
}
