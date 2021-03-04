using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Length { get; set; }

        public string Synopsis { get; set; }

        public int Year { get; set; }

        public string Genre { get; set; }

        public string ParentalRating { get; set; }

        public bool Is3D { get; set; }

        public string PosterUrl { get; set; }
    }
}
