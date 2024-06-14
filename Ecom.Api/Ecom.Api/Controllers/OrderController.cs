using Ecom.Api.Entity.Entity;
using Ecom.Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrderController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult> GetOrders(int id)
        {
            List<Order> items = new List<Order>();
            items = await _repository.GetOrders(id);
            if (items == null)
            {
                return NotFound("Orders not available!");
            }
            return Ok(items);
        }

        [HttpGet]
        [Route("GetOrderItems")]
        public async Task<IActionResult> GetOrderItems(int id)
        {
            List<OrderItem> items = new List<OrderItem>();
            items = await _repository.GetOrderItems(id);
            if (items == null)
            {
                return NotFound("Order Items not available!");
            }
            return Ok(items);
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            await _repository.CreateOrder(order);
            return Ok(order);
        }

        [HttpPost]
        [Route("AddOrderItem")]
        public async Task<IActionResult> AddOrderItem([FromBody] OrderItem orderItem)
        {
            await _repository.AddOrderItem(orderItem);
            return Ok();
        }
    }
}
