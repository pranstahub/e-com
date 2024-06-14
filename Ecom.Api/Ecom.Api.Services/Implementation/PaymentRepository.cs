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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPayment(Payment pay)
        {
            _context.Payments.Add(pay);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePayment(int id)
        {
            var pay = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(pay);
            await _context.SaveChangesAsync();
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            return await _context.Payments.FindAsync(id);
        }
    }
}