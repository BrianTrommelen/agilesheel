using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agilesheel.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        [Required]
        public Movie Movie { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }
    }
}
