using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace agilesheel.Models
{
    public class Show
    {
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public int TheaterId { get; set; }

        public Theater Theater { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public void SetDuration(Movie length) { }
    }
}
