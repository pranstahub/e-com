using Ecom.Client.Core.HttpClients;
using Ecom.Client.DTO.Request;
using Ecom.Client.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Net.Http;

namespace Ecom.Client.Controllers
{
    [Authorize]

    public class OrderController : Controller
    {

        private readonly IGenericHttpClient _httpClient;
        private readonly IToastNotification _toast;
        private readonly IConfiguration _configuration;

        public OrderController(IGenericHttpClient httpClient, IToastNotification toast, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _toast = toast;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            int customerId = Convert.ToInt32(User.FindFirst("CustomerId").Value);
            List<OrderDTOResponse> orderList = new List<OrderDTOResponse>();
            orderList = await _httpClient.GetIdAsync<List<OrderDTOResponse>>(ApiConstants.GetOrders, customerId);
            return View(orderList);
        }

        public async Task<IActionResult> Details(int orderId)
        {
            ViewBag.URL = _configuration["ApiClient:ClientUrl"] + "" + "cdn";
            List<OrderItemDTOResponse> orderItemList = new List<OrderItemDTOResponse>();
            orderItemList = await _httpClient.GetIdAsync<List<OrderItemDTOResponse>>(ApiConstants.GetOrderItems, orderId);
            return View(orderItemList);
        }

        public async Task<IActionResult> AddOrder()
        {
            var customaId = Convert.ToInt32(User.FindFirst("CustomerId").Value);

            var cart = await _httpClient.GetIdAsync<CartDTOResponse>(ApiConstants.GetCart, customaId);

            OrderDTORequest order = new OrderDTORequest()
            {
                customerId = customaId,
                total = cart.total,
                quantity = cart.quantity,
                orderDate = DateTime.Now
            };
            var ordered = await _httpClient.PostByAsync<OrderDTOResponse>(ApiConstants.CreateOrder, order);

            List<CartItemDTOResponse> cartList = new List<CartItemDTOResponse>();
            cartList = await _httpClient.GetIdAsync<List<CartItemDTOResponse>>(ApiConstants.GetCartItems, cart.id);

            foreach (var item in cartList)
            {
                OrderItemDTORequest orderItem = new OrderItemDTORequest()
                {
                    orderId = ordered.id,
                    productId = item.productId,
                    quantity = item.quantity,
                    price = item.price,
                    itemTotal = item.itemTotal
                };
                await _httpClient.PostByAsync<ActionResult>(ApiConstants.AddOrderItem, orderItem);
            }

            await _httpClient.PostByAsync<ActionResult>(ApiConstants.EmptyCart, customaId);

            _toast.AddSuccessToastMessage("Order Placed Successfully!");
            return RedirectToAction("Index", "Home");

        }

    }
}
