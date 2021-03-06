using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Models
{
    public interface IStoreRepository
    {
        IQueryable<Movie> Movies { get; }
        IQueryable<Show> Shows { get; }
        IQueryable<Ticket> Tickets { get; }
    }
}
