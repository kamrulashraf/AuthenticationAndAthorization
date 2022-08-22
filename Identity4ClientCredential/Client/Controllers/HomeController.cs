using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("/home")]
        public async Task<IActionResult> Index()
        {

            
            
            
            var client = _httpClientFactory.CreateClient();

            var discoveryDocument = await client.GetDiscoveryDocumentAsync("https://localhost:7288/");

            //var tokenClient = new TokenClient(discoveryDocument.TokenEndpoint, "client_id", "client_secret");

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {

                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client_id",
                ClientSecret = "client_secret",
                Scope = "ApiOne"
            });

            var apiClient = _httpClientFactory.CreateClient();

            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("https://localhost:7083/WeatherForecast");
            var content = await response.Content.ReadAsStringAsync();

            return Ok(new
            {
                access_token = tokenResponse.AccessToken,
                message = content
            });
        }

    }
}
