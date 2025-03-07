using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCrudApp.Model;
using ProductCrudApp.Service;

namespace ProductCrudApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAllCategories([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var categories = _categoryService.GetAllCategories(pageNumber, pageSize);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching categories.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                var category = _categoryService.GetCategoryById(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching category by id.", error = ex.Message });
            }

        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }

                _categoryService.AddCategory(category);
                return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating category.", error = ex.Message });
            }

        }

        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }

                var existingCategory = _categoryService.GetCategoryById(category.CategoryId);
                if (existingCategory == null)
                {
                    return NotFound();
                }

                _categoryService.UpdateCategory(category);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating category.", error = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var category = _categoryService.GetCategoryById(id);
                if (category == null)
                {
                    return NotFound();
                }

                _categoryService.DeleteCategory(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "An error occurred while deleting category.", error = ex.Message });
            }


        }

    }
}
