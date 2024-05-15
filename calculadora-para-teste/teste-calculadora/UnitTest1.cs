using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System.Net;

namespace teste_calculadora
{
    public class CalculatorIntegrationTests
    {
        private readonly HttpClient _client;

        public CalculatorIntegrationTests()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7030/")
            };
        }

        [Theory]
        [InlineData("soma", 2, 3, 5)]
        [InlineData("subtracao", 5, 3, 2)]
        [InlineData("multiplicacao", 4, 3, 12)]
        [InlineData("divisao", 10, 2, 5)]
        [InlineData("divisao", 10, 0, double.NaN)] // Teste para divisão por zero
        public async Task Test_Calculate(string operation, double value1, double value2, double expected)
        {
            // Arrange
            var url = $"/calculate/{operation}/{value1}/{value2}";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            if (double.IsNaN(expected))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
            else
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JToken.Parse(responseString).Value<double>();
                Assert.Equal(expected, result);
            }
        }

        [Fact]
        public async Task Test_Calculate_InvalidOperation()
        {
            // Arrange
            var url = "/calculate/invalido/10/2";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
