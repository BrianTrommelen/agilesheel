using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Models
{
    public class Show
    {
        private DateTime StartTime { get; set; }
        private TimeSpan Duration { get; set; }
        private Movie Movie { get; set; }
        private Theater Theather { get; set; }

        //public void SetDuration(MovieModel lenght);
    }
}
