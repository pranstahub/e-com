using Ecom.Api.Entity.Entity;
using Ecom.Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _repository;

        public AddressController(IAddressRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetAllAddresses")]
        public async Task<IActionResult> GetAllAddresses()
        {
            List<Address> addy = new List<Address>();
            addy = await _repository.GetAllAddresses();
            if (addy == null)
            {
                return NotFound("Addresses not available!");
            }
            return Ok(addy);
        }

        [HttpPost]
        [Route("AddAddress")]
        public async Task<IActionResult> AddAddress([FromBody] Address addy)
        {
            await _repository.AddAddress(addy);
            return Ok();
        }


        [HttpPut]
        [Route("EditAddress")]
        public async Task<IActionResult> EditAddress([FromBody] Address addy)
        {
            await _repository.EditAddress(addy);
            return Ok();
        }


        [HttpGet]
        [Route("GetAddressById")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            return Ok(await _repository.GetAddressById(id));
        }

        [HttpDelete]
        [Route("DeleteAddress")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            await _repository.DeleteAddress(id);
            return Ok();
        }
    }
}
