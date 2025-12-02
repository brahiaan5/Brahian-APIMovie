using System.ComponentModel.DataAnnotations;

namespace API.Movies.DAL.Models
{
    public class Movie : AuditBase
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Duration { get; set; }

        public string? Description { get; set; }

        [Required]
        [MaxLength(10)]
        public string Clasification { get; set; }
    }
}
