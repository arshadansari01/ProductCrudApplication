using ProductCrudApp.Model;

namespace ProductCrudApp.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Object> GetAllProducts(int pageNumber=1, int pageSize=10);
        Object GetProductById(int id);
        Product AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        void SaveChanges();

    }
}
