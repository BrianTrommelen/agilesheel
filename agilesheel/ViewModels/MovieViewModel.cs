using agilesheel.Models;
using System.Collections.Generic;

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
        public List<Review> Reviews { get; set; }
        public Review Review { get; set; }
    }
}
