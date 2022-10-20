using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace JavaScriptClient;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
                options.DefaultSignOutScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://localhost:5001";

                options.ClientId = "javascriptclient";
                options.ClientSecret = "secret";
                options.ResponseType = "code";

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.Scope.Add("testscope");
                options.Scope.Add("offline_access");
                options.Scope.Add("color");

                options.ClaimActions.MapJsonKey("email_verified", "email_verified");
                options.ClaimActions.MapJsonKey("favorite_color", "favorite_color");

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
            });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();

        app.UseBff();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBffManagementEndpoints();

            // Uncomment this for Controller support
            // endpoints.MapControllers()
            //     .AsBffApiEndpoint();

            endpoints.MapGet("/local/identity", LocalIdentityHandler)
                .AsBffApiEndpoint();

            // Effing hell, this actually acts as a proxy mapping exact endpoint to endpoint
            endpoints.MapRemoteBffApiEndpoint("/remote", "https://localhost:5002/api/Identity")
                    .RequireAccessToken(Duende.Bff.TokenType.User);
        });

        return app;
    }

    [Authorize]
    static IResult LocalIdentityHandler(ClaimsPrincipal user)
    {
        var name = user.FindFirst("name")?.Value ?? user.FindFirst("sub")?.Value;
        return Results.Json(new { message = "Local API Success!", user = name });
    }
}