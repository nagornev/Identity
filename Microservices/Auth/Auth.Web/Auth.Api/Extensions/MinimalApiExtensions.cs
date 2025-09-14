using FluentValidation;
using OperationResults;

namespace Auth.Api.Extensions
{
    public static class MinimalApiExtensions
    {
        public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder)
            where T : class
        {
            return builder.AddEndpointFilter(async (context, next) =>
            {
                var validator = context.HttpContext.RequestServices.GetRequiredService<IValidator<T>>();
                var arg = context.Arguments.FirstOrDefault(a => a is T) as T;

                if (arg != null)
                {
                    var result = await validator.ValidateAsync(arg);
                    if (!result.IsValid)
                    {
                        var error = result.Errors.First();
                        return Result.Failure(new ResultError(1, error.ErrorMessage));
                    }
                }

                return await next(context);
            });
        }
    }
}
