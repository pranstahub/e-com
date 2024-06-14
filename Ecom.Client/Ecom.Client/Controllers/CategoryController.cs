using Ecom.Client.Core.HttpClients;
using Ecom.Client.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Ecom.Client.Controllers
{
    [Authorize]

    public class CategoryController : Controller
    {
        private readonly IGenericHttpClient _httpClient;

        public CategoryController(IGenericHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task< IActionResult> Index()
        {
            List<CategoryDTOResponse> cat = new List<CategoryDTOResponse>();
            cat = await _httpClient.GetAsync<CategoryDTOResponse>(ApiConstants.GetAllCategories);
            return View(cat);
        }
    }
}
