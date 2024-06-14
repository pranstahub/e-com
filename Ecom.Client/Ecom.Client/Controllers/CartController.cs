using Ecom.Client.Core.HttpClients;
using Ecom.Client.DTO.Request;
using Ecom.Client.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Ecom.Client.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IGenericHttpClient _httpClient;
        private readonly IToastNotification _toast;
        private readonly IConfiguration _configuration;

        public CartController(IGenericHttpClient httpClient, IToastNotification toast, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _toast = toast;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.URL = _configuration["ApiClient:ClientUrl"] + "" + "cdn";

            int customerId = Convert.ToInt32(User.FindFirst("CustomerId").Value);
            var cart = await _httpClient.GetIdAsync<CartDTOResponse>(ApiConstants.GetCart, customerId);

            List<CartItemDTOResponse> cartList = new List<CartItemDTOResponse>();
            cartList = await _httpClient.GetIdAsync<List<CartItemDTOResponse>>(ApiConstants.GetCartItems, cart.id);
            return View(cartList);
        }

        public async Task<IActionResult> AddItem(int id ) 
        {
            int customerId = Convert.ToInt32(User.FindFirst("CustomerId").Value);

            var cart = await _httpClient.GetIdAsync<CartDTOResponse>(ApiConstants.GetCart, customerId);
            int cartID = cart.id;

            var product = await _httpClient.GetIdAsync<ProductDTOResponse>(ApiConstants.GetProduct, id);

            CartItemDTORequest cartItem = new CartItemDTORequest()
            {
                productId = id,
                cartId = cartID,
                quantity = 1,
                price = product.price,
                itemTotal = product.price
                //image = product.image
            };
            await _httpClient.PostByAsync<ActionResult>(ApiConstants.AddCartItem, cartItem);
            _toast.AddSuccessToastMessage("Item Added to Cart!");
            return RedirectToAction("Shop", "Product");

        }

        public async Task<IActionResult> RemoveItem(int id)
        {
            await _httpClient.PostByAsync<ActionResult>(ApiConstants.RemoveCartItem, id);
            _toast.AddSuccessToastMessage("Item Removed from Cart!");
            return RedirectToAction("Index");
        }
    }
}
