using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace agilesheel.Models
{
    public class Theater
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Has3D { get; set; }

        public ICollection<SeatRow> SeatRows { get; set; }

        public ICollection<Show> Shows { get; set; }

        public Cinema Cinema { get; set; }
    }
}
