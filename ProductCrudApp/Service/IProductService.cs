using ProductCrudApp.Model;

namespace ProductCrudApp.Service
{
    public interface IProductService
    {
        IEnumerable<Object> GetAllProducts(int pageNumber=1, int pageSize =10);
        Object GetProductById(int id);
        Product AddProduct(Product product);
        void DeleteProduct(int id);   
        void UpdateProduct(Product product);

    }
}
