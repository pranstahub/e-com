using Ecom.Api.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Services.Interface
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomers();
        Task AddCustomer(Customer customer);
        Task EditCustomer(Customer customer);
        Task DeleteCustomer(int id);
        Task<Customer> GetCustomerById(int id);
    }
}
