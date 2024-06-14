using Ecom.Api.Entity.Entity;
using Ecom.Api.Services.Implementation;
using Ecom.Api.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers = await _repository.GetAllCustomers();
            if (customers == null)
                {
                    return NotFound("Customers not available!");
                }
            return Ok(customers);
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromForm] string customerData, [FromForm] IFormFile file)
        {
            //await _repository.AddCustomer(customer);
            //return Ok();
  
            try
            {
                // Deserialize the JSON data
                var customer = JsonConvert.DeserializeObject<Customer>(customerData);

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
                    customer.image = uniqueFileName;
                    if (customer == null)
                    {
                        return BadRequest("Product data is required.");
                    }
                }
                try
                {
                    await _repository.AddCustomer(customer);
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
        [Route("EditCustomer")]
        public async Task<IActionResult> EditCustomer([FromBody] Customer customer)
        {
            await _repository.EditCustomer(customer);
            return Ok();
        }


        [HttpGet]
        [Route("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            return Ok(await _repository.GetCustomerById(id));
        }

        [HttpDelete]
        [Route("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _repository.DeleteCustomer(id);
            return Ok();
        }
    }
}