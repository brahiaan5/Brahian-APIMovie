using API.Movies.DAL.Models;
using API.Movies.DAL.Models.Dtos;
using API.Movies.Repository.IRepository;
using API.Movies.Services.IServices;
using AutoMapper;

namespace API.Movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<MovieDto> CreateMovieAsync(MovieCreateDto movieDto)
        {
            if (await _movieRepository.MovieExistsAsync(movieDto.Name))
            {
                throw new InvalidOperationException($"Movie with name {movieDto.Name} already exists");
            }

            var movie = _mapper.Map<Movie>(movieDto);
            if (!await _movieRepository.CreateMovieAsync(movie))
            {
                throw new Exception($"Something went wrong when saving the movie {movie.Name}");
            }

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<bool> DeleteMovieAsync(int movieId)
        {
            if (!await _movieRepository.MovieExistsAsync(movieId))
            {
                throw new InvalidOperationException($"Movie with id {movieId} does not exist");
            }

            if (!await _movieRepository.DeleteMovieAsync(movieId))
            {
                throw new Exception($"Something went wrong when deleting the movie {movieId}");
            }

            return true;
        }

        public async Task<MovieDto?> GetMovieAsync(int movieId)
        {
            var movie = await _movieRepository.GetMovieAsync(movieId);
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<ICollection<MovieDto>> GetMoviesAsync()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            return _mapper.Map<ICollection<MovieDto>>(movies);
        }

        public async Task<MovieDto> UpdateMovieAsync(int movieId, MovieCreateDto movieDto)
        {
            var movie = await _movieRepository.GetMovieAsync(movieId);
            if (movie == null)
            {
                throw new InvalidOperationException($"Movie with id {movieId} does not exist");
            }

            if (await _movieRepository.MovieExistsAsync(movieDto.Name) && movie.Name != movieDto.Name)
            {
                throw new InvalidOperationException($"Movie with name {movieDto.Name} already exists");
            }

            _mapper.Map(movieDto, movie);

            if (!await _movieRepository.UpdateMovieAsync(movie))
            {
                throw new Exception($"Something went wrong when updating the movie {movie.Name}");
            }

            return _mapper.Map<MovieDto>(movie);
        }
    }
}
