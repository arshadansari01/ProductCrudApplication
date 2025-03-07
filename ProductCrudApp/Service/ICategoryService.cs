using ProductCrudApp.Model;

namespace ProductCrudApp.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories(int pageNumber = 1, int pageSize = 10);
        Category GetCategoryById(int id);
        Category AddCategory(Category category);
        void DeleteCategory(int id);
        void UpdateCategory(Category category);
    }
}
