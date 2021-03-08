using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agilesheel.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Show")]
        public int ShowId { get; set; }

        public Show Show { get; set; }

        [ForeignKey("SeatRow")]
        public int SeatRowId { get; set; }

        public SeatRow SeatRow { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public double Price { get; set; }

        public int SeatNumber { get; set; }

        //public string GetMovieFromShow()
        //{

        //}
    }
}
