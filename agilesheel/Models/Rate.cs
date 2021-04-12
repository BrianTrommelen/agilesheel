using System.ComponentModel.DataAnnotations;

namespace agilesheel.Models
{
    public class Rate
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Conditions { get; set; }
        public double Price { get; set; }
    }
}
