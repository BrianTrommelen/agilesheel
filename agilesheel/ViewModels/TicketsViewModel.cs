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

        public List<int> Seat { get; set; }
        public Ticket Ticket { get; set; }
        public Show Show { get; set; }
        public Movie Movie { get; set; }
        public Theater Theater { get; set; }
        public List<Show> Shows { get; set; }
        public List<Movie> Movies { get; set; }
        public List<Ticket> Tickets { get; set; }

        public Show CurrentShow { get; set; }

        //Methods that require IStoreRepository
        public Movie GetMovieFromShowId(int showId)
        {
            Show = _repo.Shows
                .FirstOrDefault(m => m.Id == showId);
            return _repo.Movies
                .FirstOrDefault(m => m.Id == Show.MovieId);
        }

        public List<int> GetSeatNumber(int showId)
        {
            // get existing tickets for show
            Tickets = _repo.Tickets.Where(x => x.ShowId == showId).ToList();

            CurrentShow = _repo.Shows.FirstOrDefault(m => m.Id == showId);

            //// Get current theater where show plays
            Theater = _repo.Theaters.FirstOrDefault(m => m.Id == CurrentShow.TheaterId);

            int currentRows = _repo.SeatRows.Where(x => x.TheaterId == Theater.Id).Count();
            int counterForRows = 0;

            int totalSeats = 0;

            List<int> seatVars;

            // Count total seats in theater
            while (currentRows > counterForRows)
            {
                counterForRows++;

                // Get total seats of theater
                totalSeats += _repo.SeatRows.Where(x => x.Id == counterForRows).Select(x => x.Seats).FirstOrDefault();

                // Get seats per row
                int seatsForRow = _repo.SeatRows.Where(x => x.Id == counterForRows).Select(x => x.Seats).FirstOrDefault();

                List<Ticket> ticketsInRow = _repo.Tickets.Where(x => x.SeatRowId == counterForRows).Where(x => x.ShowId == showId).ToList();

                // If tickets exist for this row
                if (ticketsInRow != null)
                {
                    int totalTicketsInThisRow = ticketsInRow.Count;

                    // Make sure there are tickets left for this row to assign seatnumber
                    if (totalTicketsInThisRow != seatsForRow)
                    {
                        // Assign seatnumber
                        int seatNumber = ticketsInRow.Count + 1;
                        seatVars = new List<int>
                        {
                            counterForRows,
                            seatNumber
                        };

                        return seatVars;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
            }

            // if no seatnumbers available return null
            return null;
        }


        public bool IsShowInWeekDay(DateTime date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;

            if (dayOfWeek == DayOfWeek.Friday || dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            return true;
        }

        public bool IsShowInTimeSpan(TimeSpan start, TimeSpan end)
        {
            var now = DateTime.Now.TimeOfDay;

            if (start <= now && now <= end)
            {
                return true;
            }

            return false;
        }

    }
}
