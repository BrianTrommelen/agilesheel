using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Models
{
    public class ShowModel
    {
        private DateTime StartTime { get; set; }
        private TimeSpan Duration { get; set; }
        private MovieModel Movie { get; set; }
        private TheaterModel Theather { get; set; }

        //public void SetDuration(MovieModel lenght);
    }
}
