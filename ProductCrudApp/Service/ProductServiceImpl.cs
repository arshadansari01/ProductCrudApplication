using ProductCrudApp.Model;
using ProductCrudApp.Repository;

namespace ProductCrudApp.Service
{
    public class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductServiceImpl(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product AddProduct(Product product)
        {
            var result = _productRepository.AddProduct(product);
            _productRepository.SaveChanges();
            return result;
        }

        public void DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);
            _productRepository.SaveChanges();
        }

        public IEnumerable<Object> GetAllProducts(int pageNumber, int pageSize)
        {
            return _productRepository.GetAllProducts(pageNumber,pageSize);
        }

        public Object GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
            _productRepository.SaveChanges();
        }
    }
}
