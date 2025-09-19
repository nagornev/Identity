using Auth.Api.Extensions.Startup;
using Serilog;
using Serilog.Sinks.Elasticsearch;

Log.Logger = new LoggerConfiguration()
   .Enrich.FromLogContext()
   .WriteTo.Async(cfg => cfg.Console())
   .WriteTo.Async(cfg => cfg.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
    {
        AutoRegisterTemplate = true,
        IndexFormat = "logstash-{0:yyyy.MM.dd}"
    }))
   .CreateLogger();

var app = WebApplication.CreateBuilder(args)
                        .CreateApplication();

await app.StartApplicationAsync();
