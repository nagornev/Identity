using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Notification.Application.Exceptions.Infrastructures.Persistences;

namespace Notification.Persistence.Decorators
{
    public abstract class PersistenceExceptionDecorator
    {
        private static readonly int[] _persistenceIsUnavailableErrorTypes = [-1, 53, 10054, 10060];

        public async Task<T> CallAsync<T>(Func<Task<T>> callback)
        {
            try
            {
                return await callback.Invoke();
            }
            catch (InvalidOperationException exception)
            {
                LogError(exception);
                throw new PersistenceInvalidOperationInfrastructureException(exception);
            }
            catch (OperationCanceledException exception)
            {
                LogError(exception);
                throw new PersistenceOperationCanceledInfrastructureException(exception);
            }
            catch (DbUpdateException exception)
             when (exception.InnerException is SqlException sqlException &&
                   _persistenceIsUnavailableErrorTypes.Contains(sqlException.Number))

            {
                LogError(exception);
                throw new PersistenceUnavailableInfrastructureException();
            }
            catch (SqlException exception)
            {
                LogError(exception);
                throw new PersistenceInvalidOperationInfrastructureException();
            }
        }

        protected async Task CallAsync(Func<Task> callback)
        {
            try
            {
                await callback.Invoke();
            }
            catch (InvalidOperationException exception)
            {
                LogError(exception);
                throw new PersistenceInvalidOperationInfrastructureException(exception);
            }
            catch (OperationCanceledException exception)
            {
                LogError(exception);
                throw new PersistenceOperationCanceledInfrastructureException(exception);
            }
            catch (DbUpdateException exception)
             when (exception.InnerException is SqlException sqlException &&
                   _persistenceIsUnavailableErrorTypes.Contains(sqlException.Number))

            {
                LogError(exception);
                throw new PersistenceUnavailableInfrastructureException();
            }
            catch (SqlException exception)
            {
                LogError(exception);
                throw new PersistenceInvalidOperationInfrastructureException();
            }
        }

        protected T Call<T>(Func<T> callback)
        {
            try
            {
                return callback.Invoke();
            }
            catch (InvalidOperationException exception)
            {
                LogError(exception);
                throw new PersistenceInvalidOperationInfrastructureException(exception);
            }
            catch (OperationCanceledException exception)
            {
                LogError(exception);
                throw new PersistenceOperationCanceledInfrastructureException(exception);
            }
            catch (DbUpdateException exception)
             when (exception.InnerException is SqlException sqlException &&
                   _persistenceIsUnavailableErrorTypes.Contains(sqlException.Number))

            {
                LogError(exception);
                throw new PersistenceUnavailableInfrastructureException();
            }
            catch (SqlException exception)
            {
                LogError(exception);
                throw new PersistenceInvalidOperationInfrastructureException();
            }
        }

        protected abstract void LogError(Exception exception);
    }
}
