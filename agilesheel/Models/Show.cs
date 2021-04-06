using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agilesheel.Models
{
    public class Show
    {
        public Show()
        {
            Tickets = new List<Ticket>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        [Required]
        public Movie Movie { get; set; }

        [ForeignKey("Theater")]
        public int TheaterId { get; set; }

        [Required]
        public Theater Theater { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }

        public void SetDuration(Movie length) { }
    }
}
