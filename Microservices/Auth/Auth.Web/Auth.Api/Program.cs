using Auth.Api.Extensions.Startup;
using System.IdentityModel.Tokens.Jwt;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

var app = WebApplication.CreateBuilder(args)
                        .CreateApplication();

await app.StartApplicationAsync();
