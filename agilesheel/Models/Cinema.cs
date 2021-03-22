using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }

        public string FBLink { get; set; }

        public string InstaLink { get; set; }

        public ICollection<Theater> Theaters { get; set; }
    }
}
