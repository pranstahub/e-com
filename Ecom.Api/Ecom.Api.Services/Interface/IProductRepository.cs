using Ecom.Api.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Services.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task AddProduct(Product product);
        Task EditProduct(Product product);
        Task DeleteProduct(int id);
        Task<Product> GetProductById(int id);
    }
}
