using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity4ClientCredential
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> GetScopes() => 
            new List<ApiScope> { 
                new ApiScope("ApiOne")
            };


        public static IEnumerable<Client> GetClients() {
            return new List<Client> {
                new Client{
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "ApiOne" }
                }
            };
        }
    }
}
