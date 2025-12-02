using System.ComponentModel.DataAnnotations;


namespace API.Movies.DAL.Models
{
    public class AuditBase
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime UpdatedDate { get; set; }
    }
}
