using Microsoft.AspNetCore.Mvc.Testing;
using TestesMinimalAPI;
using MinimalAPI.ViewModels;
using Newtonsoft.Json;

namespace MinimalAPITestes.Controllers
{
    public class LoginControllerTest
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _httpClient;

        // Fábrica que simula um .NET Host com a hospedagem da API
        public LoginControllerTest(WebApplicationFactory<Startup> factory, HttpClient httpClient)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public void Logar()
        {
            // Instancia um novo login
            var login = new LoginViewModel
            {
                Email = "admin",
                Senha = "123"
            };

            // Serializa objeto login
            StringContent content = new StringContent(JsonConvert.SerializeObject(login));

            // Client para trafegar nas rotas
            var result = _httpClient.PostAsync("api/authentication/login", content).GetAwaiter().GetResult();

            // Retorno correto
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }
    }
}
