using System.ComponentModel.DataAnnotations;

namespace API.Movies.DAL.Models.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The category name is required")]
        [MaxLength(100, ErrorMessage = "The category name cannot exceed 100 characters")]

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate{ get; set; }
    }
}