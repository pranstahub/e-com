using Ecom.Api.Entity.Data;
using Ecom.Api.Entity.Entity;
using Ecom.Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            List<Product> product = new List<Product>();
            product = await _repository.GetAllProducts();
            if (product == null)
            {
                return NotFound("Products not available!");
            }
            return Ok(product);
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] string productData, [FromForm] IFormFile file)
        {
            try
            {
                // Deserialize the JSON data
                var product = JsonConvert.DeserializeObject<Product>(productData);

                if (file != null && file.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    // Set the image URL in the product
                    product.image = uniqueFileName;
                    if (product == null)
                    {
                        return BadRequest("Product data is required.");
                    }
                }
                try
                {
                    await _repository.AddProduct(product);
                    return Ok();
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("EditProduct")]
        public async Task<IActionResult> EditProduct([FromBody] Product pro)
        {
            await _repository.EditProduct(pro);
            return Ok();
        }


        [HttpGet]
        [Route("GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await _repository.GetProductById(id));
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _repository.DeleteProduct(id);
            return Ok();
        }
    }
}
