using API.Movies.DAL.Models.Dtos;

namespace API.Movies.Services.IServices
{
    public interface IMovieService
    {
        Task<ICollection<MovieDto>> GetMoviesAsync();
        Task<MovieDto?> GetMovieAsync(int movieId);
        Task<MovieDto> CreateMovieAsync(MovieCreateDto movieDto);
        Task<MovieDto> UpdateMovieAsync(int movieId, MovieCreateDto movieDto);
        Task<bool> DeleteMovieAsync(int movieId);
    }
}
