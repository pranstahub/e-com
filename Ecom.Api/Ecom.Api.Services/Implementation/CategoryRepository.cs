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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task AddCategory(Category cat)
        {
            _context.Categories.Add(cat);
            await _context.SaveChangesAsync();
        }

        public async Task EditCategory(Category cat)
        {
            _context.Categories.Update(cat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
    }
}
