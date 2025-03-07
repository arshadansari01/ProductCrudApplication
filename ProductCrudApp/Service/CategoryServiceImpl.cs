using ProductCrudApp.Data;
using ProductCrudApp.Model;
using ProductCrudApp.Repository;

namespace ProductCrudApp.Service
{
    public class CategoryServiceImpl : ICategoryService
    {
       private readonly ICategoryRepository _categoryRepository;
        public CategoryServiceImpl(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;   
        }

        public Category AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
            _categoryRepository.SaveChanges();
            return category;
        }

        public void DeleteCategory(int id)
        {
            _categoryRepository.DeleteCategory(id);
            _categoryRepository.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories(int pageNumber = 1, int pageSize = 10)
        {
            return _categoryRepository.GetAllCategories(pageNumber, pageSize);
        }

        public Category GetCategoryById(int id)
        {
           return _categoryRepository.GetCategoryById(id);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.UpdateCategory(category);
            _categoryRepository.SaveChanges();
        }
    }
}
