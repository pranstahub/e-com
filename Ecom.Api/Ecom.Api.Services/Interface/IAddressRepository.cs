using Ecom.Api.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Services.Interface
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAllAddresses();
        Task AddAddress(Address addy);
        Task EditAddress(Address addy);
        Task DeleteAddress(int id);
        Task<Address> GetAddressById(int id);
    }
}