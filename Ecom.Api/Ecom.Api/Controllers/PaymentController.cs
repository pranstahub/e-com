using Ecom.Api.Entity.Entity;
using Ecom.Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _repository;

        public PaymentController(IPaymentRepository repository)
        {
            _repository = repository;
        }


        [HttpPost]
        [Route("AddPayment")]
        public async Task<IActionResult> AddPayment([FromBody] Payment pay)
        {
            await _repository.AddPayment(pay);
            return Ok();
        }

        [HttpGet]
        [Route("GetPaymentById")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            return Ok(await _repository.GetPaymentById(id));
        }

        [HttpDelete]
        [Route("DeletePayment")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            await _repository.DeletePayment(id);
            return Ok();
        }
    }
}