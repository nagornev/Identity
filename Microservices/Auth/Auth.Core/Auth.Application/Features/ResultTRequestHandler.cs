using Auth.Application.Exceptions.Infrastructures;
using Auth.Domain.Exceptions.Domains;
using MediatR;
using Results;
using ApplicationException = Auth.Application.Exceptions.Applications.ApplicationException;

namespace Auth.Application.Features
{
    public abstract class ResultTRequestHandler<TRequestType, T> : IRequestHandler<TRequestType, Result<T>>
        where TRequestType : ResultTRequest<T>
    {
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
        }

        public abstract Task<T> HandleAsync(TRequestType request, CancellationToken cancellation);
    }
}
