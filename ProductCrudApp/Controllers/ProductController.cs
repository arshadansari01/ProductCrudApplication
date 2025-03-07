using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCrudApp.Model;
using ProductCrudApp.Service;
namespace ProductCrudApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAllProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var products = _productService.GetAllProducts(pageNumber, pageSize);
                return Ok(products); // Return 200 OK with the products
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching products.", error = ex.Message });
            }

        }

        [HttpGet("{id}")] // Route like /api/product/1
        public IActionResult GetProductById(int id)
        {
            try
            {
                var product = _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound(); // Return 404 Not Found
                }
                return Ok(product);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "An error occurred while fetching product by id.", error = ex.Message });
            }

        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest(); // 400 Bad Request
                }
                _productService.AddProduct(product);
                return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product); // 201 Created
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating product.", error = ex.Message });
            }

        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }

                var existingProduct = _productService.GetProductById(product.ProductId);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                _productService.UpdateProduct(product);
                return NoContent(); // 204 No Content (successful update)
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating product.", error = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var product = _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }

                _productService.DeleteProduct(id);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "An error occurred while deleting product.", error = ex.Message });
            }

        }
    }
}
