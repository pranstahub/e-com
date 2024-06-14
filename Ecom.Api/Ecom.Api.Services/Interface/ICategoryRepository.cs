using Ecom.Api.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Services.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task AddCategory(Category category);
        Task EditCategory(Category category);
        Task DeleteCategory(int id);
        Task<Category> GetCategoryById(int id);
    }
}
