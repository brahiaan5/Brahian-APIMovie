using API.Movies.DAL;
using API.Movies.DAL.Models;
using API.Movies.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API.Movies.Repository
{
    public class CategoryRepository : ICaregoryRepository


    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            var currentDate = DateTime.UtcNow;

            category.CreatedDate = currentDate;
            category.UpdatedDate = currentDate;

            await _context.Categories.AddAsync(category);
            return await SaveAsync();
        }

        public Task<bool> CategoryExistsByIdAsync(int categoryId)
        {
            return _context.Categories
                .AsNoTracking()
                .AnyAsync(c => c.Id == categoryId);
        }

        public Task<bool> CategoryExistsByNameAsync(string name)
        {
            return _context.Categories
                .AsNoTracking()
                .AnyAsync(c => c.Name == name);
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await GetCategoryAsync(categoryId);

            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            return await SaveAsync();
        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public Task<Category?> GetCategoryAsync(int categoryId)
        {
            return _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            category.UpdatedDate = DateTime.UtcNow;

            _context.Categories.Update(category);
            return await SaveAsync();
        }

        private async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}