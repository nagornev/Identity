using Auth.Application.DTOs;
using UAParser;

namespace Auth.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static RequestContext GetRequestContext(this HttpContext context)
        {
            string? ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault() ??
                         context.Connection.RemoteIpAddress?.ToString();

            var device = Parser.GetDefault()
                               .ParseDevice(context.Request.Headers["User-Agent"].ToString());

            return new RequestContext("chrome", "1.2.3.4");
        }
    }
}
