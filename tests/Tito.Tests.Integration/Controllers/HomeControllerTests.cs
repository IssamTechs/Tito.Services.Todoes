using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net.Http;
using System.Threading.Tasks;
using Tito.Services.Todoes.Api;
using Xunit; 

namespace Tito.Tests.Integration.Controllers
{
    public class HomeControllerTests: IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly HttpClient _client;

        public HomeControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task get_home_endpoint_should_return_message()
        {
            // act
            var response = await _client.GetAsync("");
            // arrange
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            // assert
            content.ShouldBe("Todo API");
        }
    }
}
