using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace VigenereCipherTests
{
    public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/CipherMethodsApi")]      
        [InlineData("/api/AppUsersApi")]           
        [InlineData("/api/v1/CipherJobsApi")]      
        [InlineData("/api/v2/CipherJobsApi")]      
        public async Task Get_Endpoints_ReturnSuccessAndJson(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode(); 

            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType?.ToString());
        }
    }
}