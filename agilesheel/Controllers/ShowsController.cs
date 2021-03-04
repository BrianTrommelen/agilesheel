using agilesheel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Controllers
{
    public class ShowsController : Controller
    {
        private readonly StoreDbContext _context;

        public ShowsController(StoreDbContext context)
        {
            _context = context;
        }

        //private ReturnShowsByMovieId(int? id)
        //{

        //    return Show;
        //}

    }
}
