using Duende.IdentityModel;
using Duende.IdentityServer.Models;

namespace FantasyFootballGame.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", "User roles", new[] { "role" })
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>()
            {
                new ApiScope("fantasy_api", "Fantasy Football Game API"),
                new ApiScope("admin_api", "Admin API Access")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>()
            {
                new Client
                {
                    ClientId = "fantasy_web",
                    ClientSecrets = { new Secret("78195A38-7267-7267-8F2E-8F4EB3FECF34".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { "openid", "profile", "email", "roles","fantasy_api" }
                },
                new Client
                {
                    ClientId = "fantasy_web_1",
                    ClientSecrets = { new Secret("78195A38-7267-7267-8F2E-8F4EB3FECF34".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "http://localhost:3000/signin-oidc" }, // Frontend redirect
                    PostLogoutRedirectUris = { "http://localhost:3000/signout-callback-oidc" },
                    AllowedCorsOrigins = { "http://localhost:3000" }, // Allow CORS for frontend
                    AllowedScopes = { "openid", "profile", "email", "roles", "fantasy_api" }
                },
                new Client
                {
                    ClientId = "admin_dashboard",
                    ClientSecrets = { new Secret("78195A38-7267-7267-8F2E-8F4EB3FECF34".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { "admin_api" }
                }
            };

        
    }
}
