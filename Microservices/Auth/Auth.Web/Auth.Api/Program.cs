using Auth.Api.Extensions.Startup;

var app = WebApplication.CreateBuilder(args)
                        .CreateApplication();

await app.StartApplicationAsync();
