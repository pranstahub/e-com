using Ecom.Client.Core.HttpClients;
using Ecom.Client.DTO.Response;
using Ecom.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

namespace Ecom.Client.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericHttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IGenericHttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task <IActionResult> Index()
        {
            ViewData["name"] = HttpContext.Session.GetString("Email");
            //ViewBag.mail = HttpContext.Session.GetString("Email");
            ViewBag.URL = _configuration["ApiClient:ClientUrl"] + "" + "cdn";

            List<ProductDTOResponse> products = new List<ProductDTOResponse>();
            products = await _httpClient.GetAsync<ProductDTOResponse>(ApiConstants.GetAllProducts);
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}