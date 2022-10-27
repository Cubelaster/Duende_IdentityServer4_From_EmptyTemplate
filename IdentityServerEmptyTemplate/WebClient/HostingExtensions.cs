using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;

namespace WebClient
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = builder.Configuration["IdentityUrl"];
                options.MetadataAddress = builder.Configuration["IdentityUrl"] + "/.well-known/openid-configuration";
                options.RequireHttpsMetadata = false;

                options.ClientId = "webclient";
                options.ClientSecret = "secret";
                options.ResponseType = "code";

                options.SaveTokens = true;

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.Scope.Add("testscope");
                options.Scope.Add("offline_access");
                options.Scope.Add("color");

                options.ClaimActions.MapJsonKey("email_verified", "email_verified");
                options.ClaimActions.MapJsonKey("favorite_color", "favorite_color");
                options.GetClaimsFromUserInfoEndpoint = true;

                options.SaveTokens = true;
            });

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            return app;
        }
    }
}