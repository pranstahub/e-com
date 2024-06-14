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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrders(int customerId)
        {
            return await _context.Orders.Where(c => c.customerId == customerId).ToListAsync();
        }

        public async Task<List<OrderItem>> GetOrderItems(int orderId)
        {
            return await _context.OrderItems.Include(x => x.product).Where(c => c.orderId == orderId).ToListAsync();
        }

        public async Task CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            Product product = await _context.Products.Where(p => p.id == orderItem.productId).FirstOrDefaultAsync();
            product.quantity -= orderItem.quantity;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
