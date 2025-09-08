using Carter;
using Hangfire;
using Hangfire.Dashboard;
using Otp.Application;
using Otp.Persistence;
using Otp.Persistence.Contexts;

namespace Otp.Api.Extensions.Startup
{
    public static class ApiStartupExtensions
    {
        public static WebApplication CreateApplication(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddOptions(configuration)
                    .AddRepositories(configuration)
                    .AddServices(configuration)
                    .AddProviders(configuration)
                    .AddFactories(configuration)
                    .AddClients(configuration)
                    .AddBackgrounds(configuration)
                    .AddCarter()
                    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssembly).Assembly))

                    .AddEndpointsApiExplorer()
                    .AddSwaggerGen();

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
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = Array.Empty<IDashboardAuthorizationFilter>()
            });
            //app.UseCors();
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
