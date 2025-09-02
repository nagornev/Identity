using Auth.Application;
using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Options;
using Auth.Persistence;
using Auth.Persistence.Contexts;
using Auth.Security;
using Carter;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.Extensions.Options;

namespace Auth.Api.Extensions.Startup
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
                    .AddValidators(configuration)
                    .AddFactories(configuration)
                    .AddStorages(configuration)
                    .AddClients(configuration)
                    .AddBackgrounds(configuration)
                    .AddCarter()
                    .AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(ApplicationAssembly).Assembly))

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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapCarter();

            await app.RunAsync();
        }

        private static async Task SeedAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                await ApplicationDbContextSeeder.SeedAsync(scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(),
                                                           scope.ServiceProvider.GetRequiredService<IOptions<ApplicationOptions>>().Value);

                await KeyStorageSeeder.SeedKeyStorageAsync(scope.ServiceProvider.GetRequiredService<IAccessKeyStorage>(),
                                                           scope.ServiceProvider.GetRequiredService<IAccessKeyPairFactory>());

                await KeyStorageSeeder.SeedKeyStorageAsync(scope.ServiceProvider.GetRequiredService<IRefreshKeyStorage>(),
                                                           scope.ServiceProvider.GetRequiredService<IRefreshKeyPairFactory>());

                await KeyStorageSeeder.SeedKeyStorageAsync(scope.ServiceProvider.GetRequiredService<IChannelKeyStorage>(),
                                                           scope.ServiceProvider.GetRequiredService<IChannelKeyPairFactory>());
            }
        }
    }
}
