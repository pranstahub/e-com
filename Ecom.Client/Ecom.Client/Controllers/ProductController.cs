using Ecom.Client.Core.HttpClients;
using Ecom.Client.DTO.Request;
using Ecom.Client.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Ecom.Client.Controllers
{
    [Authorize]

    public class ProductController : Controller
    {
        private readonly IGenericHttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IToastNotification _toast;

        public ProductController(IGenericHttpClient httpClient, IConfiguration configuration, IToastNotification toast)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _toast = toast;
        }
        public async Task<IActionResult> Index()
        {
            List<ProductDTOResponse> products = new List<ProductDTOResponse>();
            products = await _httpClient.GetAsync<ProductDTOResponse>(ApiConstants.GetAllProducts);
            ViewBag.URL = _configuration["ApiClient:ClientUrl"] + "" + "cdn";

            return View(products);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductDTORequest product)
        {
            var formData = new MultipartFormDataContent();
            string productData = JsonConvert.SerializeObject(product);
            formData.Add(new StringContent(productData, Encoding.UTF8, "application/json"), "productData");
            if (product.imageFile != null && product.imageFile.Length > 0)
            {
                formData.Add(new StreamContent(product.imageFile.OpenReadStream()), "file", product.imageFile.FileName);
            }
            try
            {
                await _httpClient.PostWithFileAsync<ActionResult>(ApiConstants.AddProduct, formData);
                _toast.AddSuccessToastMessage("Product Added!");
                return RedirectToAction("Index", "Product");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Shop()
        {
            List<ProductDTOResponse> products = new List<ProductDTOResponse>();
            products = await _httpClient.GetAsync<ProductDTOResponse>(ApiConstants.GetAllProducts);
            ViewBag.URL = _configuration["ApiClient:ClientUrl"] + "" + "cdn";

            return View(products);
        }

        public async Task<ActionResult> Edit(int id)
        {
            ProductDTOResponse product = new ProductDTOResponse();
            product = await _httpClient.GetIdAsync<ProductDTOResponse>(ApiConstants.GetProduct, id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductDTORequest product)
        {
            try
            {
                await _httpClient.PutByAsync<ProductDTOResponse>(ApiConstants.UpdateProduct, product);
                _toast.AddSuccessToastMessage("Changes Saved");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
