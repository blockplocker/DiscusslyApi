using Discussly.Models;
using DiscusslyApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscusslyApi.DAL
{
    public class CategoryManeger
    {
        private readonly DiscusslyApiContext _context;
        public CategoryManeger(DiscusslyApiContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Category.FindAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var tracked = await _context.Category.FindAsync(category.Id);
            if (tracked != null)
            {
                // only Updates the these properties 
                tracked.Name = category.Name;
                tracked.Description = category.Description;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);
            if (category != null)
            {
                _context.Category.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
