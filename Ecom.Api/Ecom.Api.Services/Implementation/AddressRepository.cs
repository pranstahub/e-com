using Ecom.Api.Entity.Data;
using Ecom.Api.Entity.Entity;
using Ecom.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Services.Implementation
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Address>> GetAllAddresses()
        {
            return await _context.Addresses.ToListAsync();
        }
        public async Task AddAddress(Address addy)
        {
            _context.Addresses.Add(addy);
            await _context.SaveChangesAsync();
        }

        public async Task EditAddress(Address addy)
        {
            _context.Addresses.Update(addy);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddress(int id)
        {
            var addy = await _context.Addresses.FindAsync(id);
            _context.Addresses.Remove(addy);
            await _context.SaveChangesAsync();
        }

        public async Task<Address> GetAddressById(int id)
        {
            return await _context.Addresses.FindAsync(id);
        }
    }
}
