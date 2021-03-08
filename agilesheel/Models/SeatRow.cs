using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Models
{
    public class SeatRow
    {
        [Key]
        public int Id { get; set; }

        public int Seats { get; set; }
        public int RowNumber { get; set; }

        [ForeignKey("Theater")]
        public int TheaterId { get; set; }
        public Theater Theater { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
