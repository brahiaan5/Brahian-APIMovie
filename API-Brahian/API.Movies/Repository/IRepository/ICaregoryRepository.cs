using API.Movies.DAL.Models;

namespace API.Movies.Repository.IRepository
{
    public interface ICaregoryRepository
    {
        Task<ICollection<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryAsync(int categoryId);
        Task<bool> CategoryExistsByIdAsync(int categoryId);
        Task<bool> CategoryExistsByNameAsync(string name);
        Task<bool> AddCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}