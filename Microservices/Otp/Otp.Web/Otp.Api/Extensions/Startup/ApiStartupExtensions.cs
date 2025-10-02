using Carter;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.OpenApi.Models;
using Notification.Api.Extensions.Startup;
using Otp.Application;
using Otp.Persistence;
using Otp.Persistence.Contexts;
using Serilog;

namespace Otp.Api.Extensions.Startup
{
    public static class ApiStartupExtensions
    {
        public static WebApplication CreateApplication(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddOptions(configuration)
                    .AddAuth(configuration)
                    .AddRepositories(configuration)
                    .AddServices(configuration)
                    .AddProviders(configuration)
                    .AddFactories(configuration)
                    .AddClients(configuration)
                    .AddBackgrounds(configuration)
                    .AddValidators(configuration)
                    .AddLogger(configuration)
                    .AddCarter()
                    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssembly).Assembly))

                    .AddEndpointsApiExplorer()
                    .AddSwaggerGen();

            builder.Host.UseSerilog();

            return builder.Build();
        }

        public static async Task StartApplicationAsync(this WebApplication app)
        {
            await SeedAsync(app);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard("/api/otp/hangfire");
            app.MapCarter();

            await app.RunAsync();
        }

        private static async Task SeedAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                await ApplicationDbContextSeeder.SeedAsync(scope.ServiceProvider.GetRequiredService<ApplicationDbContext>());
            }
        }
    }
}
