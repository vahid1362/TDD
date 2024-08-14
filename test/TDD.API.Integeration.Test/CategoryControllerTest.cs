using FluentAssertions;
using System.Net;


namespace TDD.API.Integeration.Test
{
    [Collection("Integration Tests")]
    public class CategoryControllerTest : TestBase
    {
        public CategoryControllerTest(TestServer server) : base(server)
        {
        }

        [Fact]
        public async Task AdminLoginShouldReturnToken()
        {
            var response = await _client.GetAsync("/api/category");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var token = await response.Content.ReadAsStringAsync();
            token.Should().NotBeNullOrEmpty();
        }
    }
}
