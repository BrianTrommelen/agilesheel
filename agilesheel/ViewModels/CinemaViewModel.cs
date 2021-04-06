using agilesheel.Models;
using System.Collections.Generic;

namespace agilesheel.ViewModels
{
    public class CinemaViewModel
    {
        public CinemaViewModel(IStoreRepository repo)
        {
            _repo = repo;
        }
        public CinemaViewModel()
        {
            _repo = null;
        }

        private IStoreRepository _repo;
        public Cinema Cinema { get; set; }
        public List<Cinema> Cinemas { get; set; }        
    }
}
