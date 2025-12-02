using System.ComponentModel.DataAnnotations;

namespace API.Movies.DAL.Models.Dtos
{
    public class MovieCreateDto
    {
        [Required(ErrorMessage = "The name is required")]
        [MaxLength(100, ErrorMessage = "The name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The duration is required")]
        public int Duration { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "The clasification is required")]
        [MaxLength(10, ErrorMessage = "The clasification cannot exceed 10 characters")]
        public string Clasification { get; set; }
    }
}
