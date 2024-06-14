using Ecom.Api.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Services.Interface
{
    public interface IOrderRepository
    {
        //Task<Order> GetOrder(int orderId);
        Task<List<Order>> GetOrders(int customerId);
        Task<List<OrderItem>> GetOrderItems(int orderId);

        Task CreateOrder(Order order);
        Task AddOrderItem(OrderItem orderItem);
    }
}
