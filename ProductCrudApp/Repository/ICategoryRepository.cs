using Microsoft.AspNetCore.Mvc;
using ProductCrudApp.Model;

namespace ProductCrudApp.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories(int pageNumber = 1, int pageSize = 10);
        Category GetCategoryById(int id);
        Category AddCategory(Category category);  
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
        void SaveChanges();

    }
}
