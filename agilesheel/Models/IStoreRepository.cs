using System.Linq;

namespace agilesheel.Models
{
    public interface IStoreRepository
    {
        IQueryable<Movie> Movies { get; }
        IQueryable<Theater> Theaters { get; }
        IQueryable<Show> Shows { get; }
        IQueryable<Ticket> Tickets { get; }
        IQueryable<SeatRow> SeatRows { get; }
        IQueryable<TextBar> TextBar { get; }
        IQueryable<LostAndFound> LostAndFound { get; }
    }
}
