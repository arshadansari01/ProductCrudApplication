using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductCrudApp.Data;
using ProductCrudApp.Model;
using System.Net.Http.Json;

namespace ProductCrudApp.IntegrationTests
{
    public class ProductControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient ?_client;
        private readonly ApplicationDbContext? _context;

        public ProductControllerTest(WebApplicationFactory<Program> factory)
        {
            var appFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove existing DBContext
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    // Add InMemory Database for Testing
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });

                    // Build the service provider
                    var serviceProvider = services.BuildServiceProvider();

                    // Seed the database
                    using var scope = serviceProvider.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();

                    // Add test data
                    db.Products.Add(new Product { Name = "Sample Product", Price = 50, CategoryId = 1, Description="Peter" });
                    db.SaveChanges();
                });
            });

            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreatedProduct()
        {

            var newProduct = new Product {ProductId=12, Name = "Test Product", Price = 99, CategoryId = 1, Description="Ravan" };
            var response = await _client.PostAsJsonAsync("/api/product", newProduct);

            response.EnsureSuccessStatusCode();
            var createdProduct = await response.Content.ReadFromJsonAsync<Product>();
            Assert.NotNull(createdProduct);
            Assert.Equal("Test Product", createdProduct.Name);
        }

        [Fact]
        public async Task GetProducts_ReturnsListOfProducts()
        {
            var response = await _client.GetAsync("/api/product");

            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            Assert.NotNull(products);
            Assert.True(products.Count > 0);
        }


        [Fact]
        public async Task UpdateProduct_ReturnsUpdatedProduct()
        {
            System.Diagnostics.Debugger.Launch();

            var response = await _client.GetAsync("/api/product/12");

            response.EnsureSuccessStatusCode();
            var product = await response.Content.ReadFromJsonAsync<object>();
            System.Diagnostics.Debugger.Launch();
            //var getResponse = await _client.GetAsync("/api/product/12");
            var updatedProduct = new Product { Name = "Updated Product", Price = 149, CategoryId = 1, Description="Ultron" };
          
            var updatedResponse = await _client.PutAsJsonAsync("/api/product/12", updatedProduct);

            updatedResponse.EnsureSuccessStatusCode();
             //var product = await response.Content.ReadFromJsonAsync<Product>();
             Assert.NotNull(product);
            Assert.Equal(System.Net.HttpStatusCode.NoContent, updatedResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_RemovesProduct()
        {
            var response = await _client.DeleteAsync("/api/product/1");

            response.EnsureSuccessStatusCode();
            var getResponse = await _client.GetAsync("/api/product/1");
            Assert.Equal(System.Net.HttpStatusCode.OK, getResponse.StatusCode);
        }
    }




}
