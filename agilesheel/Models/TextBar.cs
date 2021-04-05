using System.ComponentModel.DataAnnotations;

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
