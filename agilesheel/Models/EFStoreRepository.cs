using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private AgilesheelContext context;

        public EFStoreRepository(AgilesheelContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Movie> Movies => context.Movies;
        public IQueryable<Theater> Theaters => context.Theaters;
        public IQueryable<Show> Shows => context.Shows;
        public IQueryable<Ticket> Tickets => context.Tickets;
        public IQueryable<SeatRow> SeatRows => context.SeatRows;
    }
}
