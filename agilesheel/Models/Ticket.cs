using System.ComponentModel.DataAnnotations;

namespace agilesheel.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public Show Show { get; set; }

        public SeatRow SeatRow { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public double Price { get; set; }

        public int SeatNumber { get; set; }
    }
}
