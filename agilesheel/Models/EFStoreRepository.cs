using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext context;

        public EFStoreRepository(StoreDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Movie> Movies => context.Movies;
        public IQueryable<Theater> Theaters => context.Theaters;
        public IQueryable<Show> Shows => context.Shows;
        public IQueryable<Ticket> Tickets => context.Tickets;
        public IQueryable<SeatRow> SeatRows => context.SeatRows;
        public IQueryable<TextBar> TextBar => context.TextBar;
    }
}
