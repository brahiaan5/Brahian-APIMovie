using API.Movies.DAL.Models.Dtos;
using API.Movies.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>A list of categories</returns>
        [HttpGet(Name = "GetCategoriesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<CategoryDto>>> GetCategoriesAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Get a category by id
        /// </summary>
        /// <param name="id">Id of the category</param>
        /// <returns>A category</returns>
        /// <response code="200">Returns the category</response>
        /// <response code="404">If the category was not found</response>
        /// <response code="400">If the id is not valid</response>
        /// <response code="500">If an error occurred</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/categories/1
        /// </remarks>
        [HttpGet("{id:int}", Name = "GetCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDto>> GetCategoryAsync(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound($"Category with id {id} does not exist");
            }

            return Ok(category);
        }

        /// <summary>
        /// Add a new category
        /// </summary>
        /// <param name="category">Category to add</param>
        /// <returns>The added category</returns>
        /// <response code="201">Returns the added category</response>
        /// <response code="400">If the category is not valid</response>
        /// <response code="500">If an error occurred</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/categories
        ///     {
        ///         "name": "New Category"
        ///     }
        /// </remarks>
        [HttpPost(Name = "AddCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDto>> AddCategoryAsync([FromBody] CategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addedCategory = await _categoryService.AddCategoryAsync(dto);

                return CreatedAtRoute(
                    "GetCategoryAsync",
                    new { id = addedCategory.Id },
                    addedCategory
                );
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
        /// Update a category
        /// </summary>
        /// <param name="id">Id of the category to update</param>
        /// <param name="category">Category to update</param>
        /// <returns>The updated category</returns>
        /// <response code="200">Returns the updated category</response>
        /// <response code="400">If the category is not valid</response>
        /// <response code="404">If the category was not found</response>
        /// <response code="500">If an error occurred</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/categories/1
        ///     {
        ///         "name": "Updated Category"
        ///     }
        /// </remarks>
        [HttpPut("{id:int}", Name = "UpdateCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDto>> UpdateCategoryAsync(int id, [FromBody] CategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedCategory = await _categoryService.UpdateCategoryAsync(id, dto);

                return Ok(updatedCategory);
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
        /// Delete a category
        /// </summary>
        /// <param name="id">Id of the category to delete</param>
        /// <returns>The deleted category</returns>
        /// <response code="200">Returns the deleted category</response>
        /// <response code="404">If the category was not found</response>
        /// <response code="500">If an error occurred</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/categories/1
        /// </remarks>
        [HttpDelete("{id:int}", Name = "DeleteCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDto>> DeleteCategoryAsync(int id)
        {
            try
            {
                var deletedCategory = await _categoryService.DeleteCategoryAsync(id);

                return Ok(deletedCategory);
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