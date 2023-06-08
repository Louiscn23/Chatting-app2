using Chatting_app2.Test;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Chatting_app2
{
    public class WhenPostMessage
    {
        [Fact]
        public async Task ShouldReturnCorrectResponse()
        {
            // Arrange


            // Act
            await using var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(_ =>
                    {
                        var dbContext = new MockedDb().CreateDbContext();
                        return dbContext;
                    });

                    services.BuildServiceProvider();
                });
            });
            ;

            using var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/Message", new MessageDTO
            {
                MessageText = "test",
                Username = "testusernmae",
                MessageTime = DateTime.Now
            });
            var content = await result.Content.ReadFromJsonAsync<MessageDTO>();

            //Assert
            Assert.NotNull(content);
            Assert.Equal("test", content?.MessageText);
        }
    }
}
