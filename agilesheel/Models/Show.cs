using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace agilesheel.Models
{
    public class Show
    {
        private int Id { get; set; }
        private DateTime StartTime { get; set; }
        private TimeSpan Duration { get; set; }

        //private ICollection<Movie> Movies { get; set; }
        //private Theater Theather { get; set; }

        //public void SetDuration(MovieModel lenght);
    }
}
