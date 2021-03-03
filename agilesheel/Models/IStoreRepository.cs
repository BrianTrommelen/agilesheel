using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Models
{
    public interface IStoreRepository
    {
        IQueryable<MovieModel> Movies { get; }
    }
}
