using API.Movies.DAL.Models.Dtos;

namespace API.Movies.Services.IServices
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto?> GetCategoryAsync(int categoryId);
        Task<CategoryDto> AddCategoryAsync(CategoryCreateDto categoryDto);
        Task<CategoryDto> UpdateCategoryAsync(int categoryId, CategoryCreateDto categoryDto);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}