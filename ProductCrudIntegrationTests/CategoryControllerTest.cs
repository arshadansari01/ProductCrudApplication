using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductCrudApp.Data;
using ProductCrudApp.Model;
using System.Net.Http.Json;

namespace ProductCrudApp.IntegrationTests
{
    public class CategoryControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CategoryControllerTest(WebApplicationFactory<Program> factory)
        {
            var appFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestCategoryDb");
                    });

                    var serviceProvider = services.BuildServiceProvider();

                    using var scope = serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();

                    db.Categories.Add(new Category { Name = "Electronics", Description="Ultron" , Products= null});
                    db.SaveChanges();
                });
            });

            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task CreateCategory_ReturnsCreatedCategory()
        {
            var newCategory = new Category { CategoryId = 2, Name = "Books", Description="Sherlock Holmes" };
            var response = await _client.PostAsJsonAsync("/api/category", newCategory);

            response.EnsureSuccessStatusCode();
            var createdCategory = await response.Content.ReadFromJsonAsync<Category>();

            Assert.NotNull(createdCategory);
            Assert.Equal("Books", createdCategory.Name);
        }

        [Fact]
        public async Task GetCategories_ReturnsListOfCategories()
        {
            var response = await _client.GetAsync("/api/category");

            response.EnsureSuccessStatusCode();
            var categories = await response.Content.ReadFromJsonAsync<List<Category>>();

            Assert.NotNull(categories);
            Assert.True(categories.Count > 0);
        }

        [Fact]
        public async Task UpdateCategory_ReturnsNoContent()
        {
            var updatedCategory = new Category { CategoryId = 2, Name = "Updated Electronics" };
            System.Diagnostics.Debugger.Launch();
            var response = await _client.PutAsJsonAsync("/api/category", updatedCategory);
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteCategory_RemovesCategory()
        {
            var response = await _client.DeleteAsync("/api/category/1");
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
