using Auth.Application;
using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Options;
using Auth.Persistence;
using Auth.Persistence.Contexts;
using Auth.Security;
using Carter;
using Hangfire;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

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
                    .AddConfigures(configuration)
                    .AddLogger(configuration)
                    .AddCarter()
                    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssembly).Assembly))

                    .AddEndpointsApiExplorer()
                    .AddSwaggerGen(options =>
                    {
                        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer",
                            BearerFormat = "JWT",
                            In = ParameterLocation.Header,
                            Description = "Введите JWT токен в формате: Bearer {your token}"
                        });

                        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] {}
                            }
                        });
                    });

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
            app.UseHangfireDashboard("/api/identity/hangfire");
            app.MapCarter();

            await app.RunAsync();
        }

        private static async Task SeedAsync(WebApplication app)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

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
