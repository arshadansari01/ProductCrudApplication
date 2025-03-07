using Microsoft.EntityFrameworkCore;
using ProductCrudApp.Data;
using ProductCrudApp.Model;

namespace ProductCrudApp.Repository
{
    public class CategoryRepositoryImpl : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepositoryImpl(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public Category AddCategory(Category category)
        {
            _context.Categories.Add(category);

            return category;
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
        }

        public IEnumerable<Category> GetAllCategories(int pageNumber = 1, int pageSize = 10)
        {
            // Calculate the skip value for pagination
            var skip = (pageNumber - 1) * pageSize;

            // Apply pagination and include related Category names
            var categories = _context.Categories
                .Skip(skip) // Skip the records for the current page
                .Take(pageSize) //
                .Include(c=>c.Products).ToList();

            return categories;
        }

        public Category GetCategoryById(int id)
        {
            var category = _context.Categories
       .Include(c => c.Products) // Include related Products
       .FirstOrDefault(c => c.CategoryId == id); // Find by CategoryId
                if (category == null)
                {
                    throw new KeyNotFoundException("Category not found");
                }

            return category;
        
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
