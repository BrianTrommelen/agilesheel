using agilesheel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;using agilesheel.ViewModels;

namespace agilesheel.ViewModels
{
    public class MovieViewModel
    {

        public MovieViewModel(IStoreRepository repo)
        {
            _repo = repo;
        }
        public MovieViewModel()
        {
            _repo = null;
        }

        private IStoreRepository _repo;

        public Show Show { get; set; }
        public Ticket Ticket { get; set; }
        public Movie Movie { get; set; }
        public List<Show> Shows { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<Movie> Movies { get; set; }
        public List<Movie> FeaturedMovies { get; set; }
        public TextBar TextBar { get; set; }
    }
}
