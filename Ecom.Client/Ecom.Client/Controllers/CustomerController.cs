using Ecom.Client.Core.Common;
using Ecom.Client.Core.HttpClients;
using Ecom.Client.DTO.Request;
using Ecom.Client.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Security.Claims;
using System.Text;

namespace Ecom.Client.Controllers
{
    [Authorize]

    public class CustomerController : Controller
    {
        private readonly IGenericHttpClient _httpClient;
        private readonly IToastNotification _toast;
        private readonly IConfiguration _configuration;

        public CustomerController(IGenericHttpClient httpClient, IToastNotification toast, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _toast = toast;
            _configuration = configuration;
        }


        public async Task<ActionResult> Index()
        {
            ViewBag.URL = _configuration["ApiClient:ClientUrl"] + "" + "cdn";
            List<CustomerResponse> customers = new List<CustomerResponse>();
            customers = await _httpClient.GetAsync<CustomerResponse>(ApiConstants.GetAllCustomers);
            return View(customers);
        }

        public async Task<IActionResult> ViewProfile()
        {
            ViewBag.URL = _configuration["ApiClient:ClientUrl"] + "" + "cdn";

            int customerId = Convert.ToInt32(User.FindFirst("CustomerId").Value);
            var emp = await _httpClient.GetIdAsync<CustomerResponse>(ApiConstants.GetCustomer, customerId);
            return View(emp);
        }

        public async Task<ActionResult> Details(int id)
        {
            var userId = User.FindFirst("UserId").Value;
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var e_mail = HttpContext.Session.GetString("Email");
            CustomerResponse customers = new CustomerResponse();
            var response = await _httpClient.GetIdAsync<Result<CustomerResponse>>(ApiConstants.GetCustomer, id);
            if (response.IsError)
            {
                _toast.AddErrorToastMessage(response.Errors.First().ErrorMessage);
                return View();
            }
            customers = response.Response;
            return View(customers);
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerDTORequest customer)
        {
            var formData = new MultipartFormDataContent();
            var email = HttpContext.Session.GetString("Email");
            customer.customerSince = DateTime.Now;
            customer.email = email;
            string customerData = JsonConvert.SerializeObject(customer);
            formData.Add(new StringContent(customerData, Encoding.UTF8, "application/json"), "customerData");
            if (customer.imageFile != null && customer.imageFile.Length > 0)
            {
                formData.Add(new StreamContent(customer.imageFile.OpenReadStream()), "file", customer.imageFile.FileName);
            }
            try
            {
                await _httpClient.PostWithFileAsync<ActionResult>(ApiConstants.AddCustomer, formData);
                _toast.AddSuccessToastMessage("Account Created!");

                HttpContext.Session.SetString("RegisteredStatus", "True");
                HttpContext.Session.SetString("FullName", customer.name);
                return RedirectToAction("Login", "Account");
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //    var email = HttpContext.Session.GetString("Email");
        //    customer.customerSince = DateTime.Now;
        //    customer.email = email; 
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var result = await _httpClient.PostByAsync<OkObjectResult>(ApiConstants.AddCustomer, customer);
        //            //if (result.IsError)
        //            //{
        //            //    _toast.AddErrorToastMessage("Create Failed!");
        //            //    return View();
        //            //}
        //            HttpContext.Session.SetString("RegisteredStatus", "True");
        //            HttpContext.Session.SetString("FullName", customer.name);
        //            _toast.AddSuccessToastMessage("Account Created!");
        //            return RedirectToAction("Login", "Account");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
        //    return View();
        //}


        public async Task<ActionResult> Edit(int id)
        {
            CustomerResponse customers = new CustomerResponse();
            customers = await _httpClient.GetIdAsync<CustomerResponse>(ApiConstants.GetCustomer, id);
            return View(customers);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerResponse customer)
        {
            try
            {
                await _httpClient.PutByAsync<CustomerResponse>(ApiConstants.UpdateCustomer, customer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> Delete(int id)
        {
            CustomerResponse customers = new CustomerResponse();
            customers = await _httpClient.GetIdAsync<CustomerResponse>(ApiConstants.GetCustomer, id);
            return View(customers);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PostDelete(int id)
        {
            try
            {
                await _httpClient.PostByAsync<CustomerResponse>(ApiConstants.DeleteCustomer, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
