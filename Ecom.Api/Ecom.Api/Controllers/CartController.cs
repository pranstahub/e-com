using Ecom.Api.Entity.Entity;
using Ecom.Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;

        public CartController(ICartRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetCart")]
        public async Task<IActionResult> GetCart(int id)
        {
            Cart cart = await _repository.GetCart(id);
            return Ok(cart);
        }

        [HttpGet]
        [Route("GetCartItems")]
        public async Task<IActionResult> GetCartItems(int id)
        {
            List<CartItem> items = new List<CartItem>();
            items = await _repository.GetCartItems(id);
            if (items == null)
            {
                return NotFound("Cart Items not available!");
            }
            return Ok(items);
        }


        [HttpPost]
        [Route("AddCartItem")]
        public async Task<IActionResult> AddCartItem([FromBody] CartItem item)
        {
            await _repository.AddCartItem(item);
            return Ok();
        }

        //[HttpPut]
        //[Route("UpdateCart")]
        //public async Task<IActionResult> UpdateCart(int id,  int quantity)
        //{
        //    await _repository.UpdateCart(id, quantity);
        //    return Ok();
        //}
        [HttpPost]
        [Route("UpdateCartItem")]
        public async Task<IActionResult> UpdateCartItem(int id,  int quantity )
        {
            await _repository.UpdateCartItem(id, quantity);
            return Ok();
        }


        [HttpPost]
        [Route("RemoveCartItem")]
        public async Task<IActionResult> RemoveCartItem([FromBody] int id)
        {
            await _repository.RemoveCartItem(id);
            return Ok();
        }

        [HttpPost]
        [Route("EmptyCart")]
        public async Task<IActionResult> EmptyCart([FromBody] int id)
        {
            await _repository.EmptyCart(id);
            return Ok();
        }

    }
}
