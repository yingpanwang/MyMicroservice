using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Microservice.IdentityServer4.Server
{
    public class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResourceResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(), //必须要添加，否则报无效的scope错误
                new IdentityResources.Profile()
            };
        }

        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1",IdentityServerConstants.StandardScopes.OpenId, //必须要添加，否则报forbidden错误
                  IdentityServerConstants.StandardScopes.Profile},
                },

                // resource owner password grant client
                new Client
                {
                    ClientId = "pwdClient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    //AccessTokenLifetime = 30,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1",IdentityServerConstants.StandardScopes.OpenId, //必须要添加，否则报forbidden错误
                  IdentityServerConstants.StandardScopes.Profile }
                }
            };
        }
    }
}