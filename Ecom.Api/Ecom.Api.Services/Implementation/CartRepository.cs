using Ecom.Api.Entity.Data;
using Ecom.Api.Entity.Entity;
using Ecom.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Api.Services.Implementation
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCart(int customerId)
        {
            return await _context.Carts.Where(c => c.customerId == customerId).FirstOrDefaultAsync();
        }

        public async Task<CartItem> GetCartItemById(int cartItemId)
        {
            return await _context.CartItems.FindAsync(cartItemId);
        }

        public async Task<List<CartItem>> GetCartItems(int cartId)
        {
            return await _context.CartItems.Include(x => x.product).Where(c => c.cartId == cartId).ToListAsync();
        }

        public async Task AddCartItem(CartItem cartItem)
        {
            var total = cartItem.price * cartItem.quantity;
            cartItem.itemTotal = total;
            _context.CartItems.Add(cartItem);
            Cart cart = await _context.Carts.Where(c => c.id == cartItem.cartId).FirstOrDefaultAsync();
            cart.total += total;
            cart.quantity += cartItem.quantity;
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProduct(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCartItem(int id, int quantity)
        {
            CartItem item = await _context.CartItems.Where(c => c.id == id).FirstOrDefaultAsync();
            var total = item.price * quantity;
            Cart cart = await _context.Carts.Where(c => c.id == item.cartId).FirstOrDefaultAsync();

            cart.total = cart.total - item.itemTotal + total;
            item.itemTotal = total;

            cart.quantity = cart.quantity - item.quantity + quantity;
            item.quantity = quantity;

            _context.CartItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCartItem(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            Cart cart = await _context.Carts.Where(c => c.id == cartItem.cartId).FirstOrDefaultAsync();

            if (cartItem != null)
            {
                cart.quantity -= cartItem.quantity;
                cart.total -= cartItem.itemTotal;
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EmptyCart(int id)
        {
            Cart cart = await _context.Carts.Where(c => c.customerId == id).FirstOrDefaultAsync();
            cart.quantity = 0;
            cart.total = 0;
            _context.SaveChanges();

            List<CartItem> items =  await _context.CartItems.Where(c => c.cartId == cart.id).ToListAsync();
            foreach(var item in items)
            {
                _context.CartItems.Remove(item); 
                _context.SaveChanges();
            }

        }
    }
}
