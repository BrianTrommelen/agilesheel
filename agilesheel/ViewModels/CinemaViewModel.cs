using agilesheel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agilesheel.ViewModels;

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
