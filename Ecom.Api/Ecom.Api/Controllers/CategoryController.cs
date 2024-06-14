using Ecom.Api.Entity.Entity;
using Ecom.Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            List<Category> category = new List<Category>();
            category = await _repository.GetAllCategories();
            if (category == null)
            {
                return NotFound("Categories not available!");
            }
            return Ok(category);
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] Category cat)
        {
            await _repository.AddCategory(cat);
            return Ok();
        }


        [HttpPut]
        [Route("EditCategory")]
        public async Task<IActionResult> EditCategory([FromBody] Category cat)
        {
            await _repository.EditCategory(cat);
            return Ok();
        }


        [HttpGet]
        [Route("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return Ok(await _repository.GetCategoryById(id));
        }

        [HttpDelete]
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _repository.DeleteCategory(id);
            return Ok();
        }
    }
}
