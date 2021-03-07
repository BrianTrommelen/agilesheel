using agilesheel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.ViewModels
{
    public class TicketsViewModel
    {

        public TicketsViewModel(IStoreRepository repo)
        {
            _repo = repo;
        }
        public TicketsViewModel()
        {
            _repo = null;
        }

        private IStoreRepository _repo;

        public int TotalSeats { get; set; }
        public Ticket Ticket { get; set; }
        public Show Show { get; set; }
        public Movie Movie { get; set; }
        public Theater Theater { get; set; }
        public List<Show> Shows { get; set; }
        public List<Movie> Movies { get; set; }
        public List<Ticket> Tickets { get; set; }

        public Show CurrentShow { get; set; }
        public List<Ticket> CurrentShowTickets { get; set; }
        public List<SeatRow> CurrentShowSeatRows { get; set; }
        public List<SeatRow> SeatRows { get; set; }

        //Methods that require IStoreRepository
        public Movie getMovieFromShowId(int showId)
        {
            return _repo.Movies
                .FirstOrDefault(m => m.Id == showId);
        }
        public Movie getCurrentRowSeats(int rowId)
        {
            return _repo.Movies
                .FirstOrDefault(m => m.Id == showId);
        }

        public int getSeatNumber(int showId)
        {
            // get tickets for show
            Tickets = _repo.Tickets.ToList();

            foreach(Ticket ticket in Tickets)
            {
                if (ticket.ShowId == showId)
                {
                    CurrentShowTickets.Add(ticket);
                }
            }

            // how many rows and seats are available?

            CurrentShow = _repo.Shows.FirstOrDefault(m => m.Id == showId);

            // Get current theater where show plays
            Theater = _repo.Theaters.FirstOrDefault(m => m.Id == CurrentShow.TheaterId);

            SeatRows = _repo.SeatRows.ToList();

            // get current seatrow from ticket to check if full?
            SeatRow = _repo.SeatRows.FirstOrDefault(m => m.Id == CurrentShow.TheaterId);

            foreach (SeatRow seatRow in SeatRows)
            {
                if (seatRow.TheaterId == CurrentShow.TheaterId)
                {
                    CurrentShowSeatRows.Add(seatRow);

                    //int i = seatRow.Seats;

                    //while (i > 0) {
                       
                    //}
                }
            }

            foreach(SeatRow seatRow in CurrentShowSeatRows)
            {
                TotalSeats += seatRow.Seats;
            }

            // send free seatnumber back


            return 8;
        }

    }
}
