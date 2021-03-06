using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace agilesheel.Models
{
    public class Show
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Movie Movie { get; set; }

        [Required]
        public Theater Theater { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }

        public void SetDuration(Movie length) { }
    }
}
