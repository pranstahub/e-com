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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            //return await _context.Products.Include(c => c.category).ToListAsync();
            //return await _context.Products.ToListAsync();
            var query = from product in _context.Products
                        join category in _context.Categories on product.categoryId equals category.id
                        select new Product
                        {
                            id = product.id,
                            name = product.name,    
                            description = product.description,
                           categoryId = product.categoryId,
                           price = product.price,
                           quantity = product.quantity,
                           rating = product.rating,
                           image = product.image
                        };

            var result = await query.ToListAsync();
            return result;


        }
        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task EditProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}