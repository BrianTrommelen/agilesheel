namespace agilesheel.Models
{
    public class Ticket
    {
        public int ShowId { get; set; }

        public Show Show { get; set; }

        public int SeatRowId { get; set; }

        public SeatRow SeatRow { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int SeatNumber { get; set; }
    }
}
