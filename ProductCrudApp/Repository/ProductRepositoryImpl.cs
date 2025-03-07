using Microsoft.EntityFrameworkCore;
using ProductCrudApp.Model;
using ProductCrudApp.Data;

namespace ProductCrudApp.Repository
{
    public class ProductRepositoryImpl : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepositoryImpl(ApplicationDbContext context)
        {
            _context = context;
        }


        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            return product;
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);

            }
        }

        public IEnumerable<Object> GetAllProducts(int pageNumber =1, int pageSize = 10)
        {
            // Calculate the skip value for pagination
            var skip = (pageNumber - 1) * pageSize;

            // Apply pagination and include related Category names
            var products = _context.Products
                .Skip(skip) // Skip the records for the current page
                .Take(pageSize) // Take only the records for the current page
                .Select(p => new
                {
                    p.ProductId,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.CategoryId,
                    CategoryName = _context.Categories
                        .Where(c => c.CategoryId == p.CategoryId)
                        .Select(c => c.Name)
                        .FirstOrDefault() // Get the category name manually
                })
                .ToList();

            return products;
        }

        public Object GetProductById(int id)
        {
            var product = _context.Products.Where(p => p.ProductId == id)
                  .Select(p => new
                  {
                      p.ProductId,
                      p.Name,
                      p.Description,
                      p.Price,
                      p.CategoryId,
                      CategoryName = _context.Categories
                .Where(c => c.CategoryId == p.CategoryId)
                .Select(c => c.Name)
                .FirstOrDefault()
                  });

            return product;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
             _context.Products.Update(product);
        }
    }
}
