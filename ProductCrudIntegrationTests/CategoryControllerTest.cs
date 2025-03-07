using Newtonsoft.Json;
using ProductCrudApp.Model;
using System.Text;

namespace ProductCrudApplication.IntegrationTests
{
    //public class CategoryControllerTest
    //{
    //    [Fact]
    //    public async Task CreateCategory_ReturnsCreatedCategory()
    //    {
    //        // Arrange
    //        var category = new Category
    //        {
    //            Name = "Electronics",
    //            Description = "This is testable item",
    //        };
    //        var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

    //        // Act
    //        var response = await _client.PostAsync("/api/category", content);

    //        // Assert
    //        response.EnsureSuccessStatusCode();
    //        var responseString = await response.Content.ReadAsStringAsync();
    //        var createdCategory = JsonConvert.DeserializeObject<Category>(responseString);
    //        Assert.Equal("Test Category", createdCategory?.Name);
    //    }

    //}
}
