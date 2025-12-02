using API.Movies.DAL.Models.Dtos;
using API.Movies.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>A list of movies</returns>
        [HttpGet(Name = "GetMoviesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<MovieDto>>> GetMoviesAsync()
        {
            try
            {
                var movies = await _movieService.GetMoviesAsync();
                return Ok(movies);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Get a movie by id
        /// </summary>
        /// <param name="id">Id of the movie</param>
        /// <returns>A movie</returns>
        [HttpGet("{id:int}", Name = "GetMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieDto>> GetMovieAsync(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieAsync(id);
                if (movie == null)
                {
                    return NotFound($"Movie with id {id} does not exist");
                }
                return Ok(movie);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Add a new movie
        /// </summary>
        /// <param name="movieDto">Movie to add</param>
        /// <returns>The added movie</returns>
        [HttpPost(Name = "CreateMovieAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieDto>> CreateMovieAsync([FromBody] MovieCreateDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdMovie = await _movieService.CreateMovieAsync(movieDto);
                return CreatedAtRoute("GetMovieAsync", new { id = createdMovie.Id }, createdMovie);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Update a movie
        /// </summary>
        /// <param name="id">Id of the movie to update</param>
        /// <param name="movieDto">Movie to update</param>
        /// <returns>The updated movie</returns>
        [HttpPut("{id:int}", Name = "UpdateMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieDto>> UpdateMovieAsync(int id, [FromBody] MovieCreateDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedMovie = await _movieService.UpdateMovieAsync(id, movieDto);
                return Ok(updatedMovie);
            }
            catch (InvalidOperationException e) when (e.Message.Contains("not exist"))
            {
                return NotFound(e.Message);
            }
            catch (InvalidOperationException e) when (e.Message.Contains("exists"))
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Delete a movie
        /// </summary>
        /// <param name="id">Id of the movie to delete</param>
        /// <returns>The deleted movie</returns>
        [HttpDelete("{id:int}", Name = "DeleteMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteMovieAsync(int id)
        {
            try
            {
                var result = await _movieService.DeleteMovieAsync(id);
                return Ok(result);
            }
            catch (InvalidOperationException e) when (e.Message.Contains("not exist"))
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
