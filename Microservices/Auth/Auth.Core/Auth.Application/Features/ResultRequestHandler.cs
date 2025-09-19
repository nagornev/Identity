using Auth.Application.Abstractions.Services;
using Auth.Application.Exceptions.Infrastructures;
using Auth.Domain.Exceptions.Domains;
using MediatR;
using OperationResults;
using ApplicationException = Auth.Application.Exceptions.Applications.ApplicationException;

namespace Auth.Application.Features
{
    public abstract class ResultRequestHandler<TRequestType> : IRequestHandler<TRequestType, Result>
        where TRequestType : ResultRequest
    {
        private readonly ILogService _logService;

        protected ResultRequestHandler(ILogService logService)
        {
            _logService = logService;
        }

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
            catch(Exception exception)
            {
                await _logService.LogError(exception, cancellationToken);

                return Result.Failure(ResultError.Create(ResultErrorTypes.Unavailable, exception.Message));
            }
        }

        public abstract Task HandleAsync(TRequestType request, CancellationToken cancellation);

    }
}
