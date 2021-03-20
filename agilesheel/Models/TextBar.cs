using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Models
{
    public class TextBar
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public bool Hide { get; set; }
    }
}
