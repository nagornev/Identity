using MediatR;
using OperationResults;
using Otp.Application.Abstractions.Services;
using Otp.Application.Exceptions.Infrastructures;
using Otp.Domain.Exceptions.Domains;
using ApplicationException = Otp.Application.Exceptions.Applications.ApplicationException;

namespace Otp.Application.Features
{
    public abstract class ResultTRequestHandler<TRequestType, T> : IRequestHandler<TRequestType, Result<T>>
        where TRequestType : ResultTRequest<T>
    {
        private readonly ILogService _logService;

        protected ResultTRequestHandler(ILogService logService)
        {
            _logService = logService;
        }

        public async Task<Result<T>> Handle(TRequestType request, CancellationToken cancellationToken)
        {
            try
            {
                T content = await HandleAsync(request, cancellationToken);

                return Result<T>.Success(content);
            }
            catch (DomainException domainException)
            {
                return domainException.GetResult<T>();
            }
            catch (ApplicationException applicationException)
            {
                return applicationException.GetResult<T>();
            }
            catch (InfrastructureException infastructureException)
            {
                return infastructureException.GetResult<T>();
            }
            catch (Exception exception)
            {
                await _logService.LogError(exception, cancellationToken);

                return Result<T>.Failure(ResultError.Create(ResultErrorTypes.Unavailable, exception.Message));
            }
        }

        public abstract Task<T> HandleAsync(TRequestType request, CancellationToken cancellation);
    }
}
