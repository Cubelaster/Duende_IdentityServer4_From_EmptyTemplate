using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityServerEmptyTemplate
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource
            {
                Name = "verification",
                DisplayName = "Verification",
                Description = "Claims used for verification",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified
                }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] {
            new ApiScope
            {
                Name = "testscope",
                DisplayName = "Test Scope",
                Description = "Scope used for testing purposes"
            }
        };

        public static IEnumerable<Client> Clients => new Client[] {
            // machine to machine client (from quickstart 1)
            new Client
            {
                ClientId = "TestClient",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "testscope" }
            },
            // interactive ASP.NET Core Web App
            new Client
            {
                ClientId = "webclient",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowOfflineAccess = true,

                AllowedGrantTypes = GrantTypes.Code,
            
                // where to redirect to after login
                RedirectUris = { "https://localhost:5003/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5003/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "verification",
                    "testscope"
                }
            }
        };
    }
}