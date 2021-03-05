using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Models
{
    public class Theater
    {
        [Key]
        public int Id { get; set; }

        public int Seats { get; set; }

        public bool Has3D { get; set; }

        public ICollection<Show> Shows { get; set; }
    }
}
