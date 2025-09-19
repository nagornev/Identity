using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace Auth.Api.Extensions.Startup
{
    public static class LogStartupExtensions
    {
        private const string _connectionStringName = "LogDbContext";

        public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(_connectionStringName)!;

            Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .WriteTo.Async(cfg => cfg.Console())
               .WriteTo.Async(cfg => cfg.Elasticsearch(new ElasticsearchSinkOptions(new Uri(connectionString))
               {
                   AutoRegisterTemplate = true,
                   IndexFormat = "auth-logstash-{0:yyyy.MM.dd}",
                   MinimumLogEventLevel = LogEventLevel.Error
               }))
               .CreateLogger();

            return services;
        }
    }
}
