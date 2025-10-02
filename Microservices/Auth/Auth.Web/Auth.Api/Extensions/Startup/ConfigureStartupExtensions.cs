using Microsoft.AspNetCore.Mvc;
using OperationResults;

namespace Auth.Api.Extensions.Startup
{
    public static class ConfigureStartupExtensions
    {
        public static IServiceCollection AddConfigures(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Configure<ApiBehaviorOptions>(options =>
                            options.InvalidModelStateResponseFactory = (context) =>
                            {
                                var entry = context.ModelState.FirstOrDefault(x => x.Value?.Errors.Count > 0);

                                var field = entry.Key;
                                var message = entry.Value!
                                                   .Errors.First()
                                                   .ErrorMessage;

                                return new BadRequestObjectResult(Result.Failure(new ResultError(default, message)));
                            });
        }
    }
}
