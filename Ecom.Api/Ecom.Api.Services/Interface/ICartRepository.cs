using Ecom.Api.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Services.Interface
{
    public interface ICartRepository
    {
        Task<Cart> GetCart(int customerId);
        Task<CartItem> GetCartItemById(int cartItemId);
        Task<List<CartItem>> GetCartItems(int cartId);
        Task AddCartItem(CartItem cartItem);
        Task UpdateCartItem(int id, int quantity);
        Task RemoveCartItem(int cartItemId);
        //Task UpdateCart(int id, int quantity);

        Task EmptyCart(int customerId);
    }
}