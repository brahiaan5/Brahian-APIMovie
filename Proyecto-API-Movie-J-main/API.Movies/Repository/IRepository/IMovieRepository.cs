using API.Movies.DAL.Models;

namespace API.Movies.Repository.IRepository
{
    public interface IMovieRepository
    {
        Task<ICollection<Movie>> GetMoviesAsync();
        Task<Movie?> GetMovieAsync(int movieId);
        Task<bool> MovieExistsAsync(string name);
        Task<bool> MovieExistsAsync(int id);
        Task<bool> CreateMovieAsync(Movie movie);
        Task<bool> UpdateMovieAsync(Movie movie);
        Task<bool> DeleteMovieAsync(int movieId);
        Task<bool> SaveAsync();
    }
}
