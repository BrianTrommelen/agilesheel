using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace agilesheel.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public ICollection<Show> Shows { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public string Title { get; set; }

        public int Length { get; set; }

        public string Synopsis { get; set; }

        public int Year { get; set; }

        public string Genre { get; set; }

        public string ParentalRating { get; set; }

        public bool Is3D { get; set; }

        public bool IsFeatured { get; set; }

        public string PosterUrl { get; set; }
    }
}
