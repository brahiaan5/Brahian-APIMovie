using API.Movies.DAL;
using API.Movies.DAL.Models;
using API.Movies.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API.Movies.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context) => _context = context;

        public async Task<bool> CreateMovieAsync(Movie movie)
        {
            var now = DateTime.UtcNow;
            movie.CreatedDate = now;
            movie.UpdatedDate = now;

            await _context.Movies.AddAsync(movie);
            return await SaveAsync();
        }

        public async Task<bool> DeleteMovieAsync(int movieId)
        {
            var movie = await GetMovieAsync(movieId);
            if (movie is null) return false;

            _context.Movies.Remove(movie);
            return await SaveAsync();
        }

        public async Task<Movie?> GetMovieAsync(int movieId) =>
            await _context.Movies
                          .AsNoTracking()
                          .FirstOrDefaultAsync(m => m.Id == movieId);

        public async Task<ICollection<Movie>> GetMoviesAsync() =>
            await _context.Movies
                          .AsNoTracking()
                          .OrderBy(m => m.Name)
                          .ToListAsync();

        public async Task<bool> MovieExistsAsync(string name) =>
            await _context.Movies
                          .AnyAsync(m => m.Name.ToLower().Trim() == name.ToLower().Trim());

        public async Task<bool> MovieExistsAsync(int id) =>
            await _context.Movies.AnyAsync(m => m.Id == id);

        public async Task<bool> SaveAsync() =>
            await _context.SaveChangesAsync() > 0;

        public async Task<bool> UpdateMovieAsync(Movie movie)
        {
            movie.UpdatedDate = DateTime.UtcNow;
            _context.Movies.Update(movie);
            return await SaveAsync();
        }
    }
}

