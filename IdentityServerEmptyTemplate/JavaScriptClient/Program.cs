using System.IdentityModel.Tokens.Jwt;
using Duende.Bff.Yarp;
using JavaScriptClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services
    .AddBff()
    .AddRemoteApis();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

app.MapGet("/", () => "Hello World!");

app.Run();
