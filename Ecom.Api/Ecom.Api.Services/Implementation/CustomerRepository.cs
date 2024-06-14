using Ecom.Api.Entity.Data;
using Ecom.Api.Entity.Entity;
using Ecom.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Services.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers.Include(c=>c.address).Include(d => d.payment).ToListAsync();
        }
        public async Task AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            Cart cart = new Cart
            {
                customerId = customer.id,
                quantity = 0,
                total = 0
            };
            _context.Carts.Add(cart);
            var user = await _context.Users.Where(x=> x.UserName == customer.email).FirstOrDefaultAsync();
            user.customerId = customer.id;
            await _context.SaveChangesAsync();
        }

        public async Task EditCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomer(int id)
        {
            var cust = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(cust);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

    }
}
