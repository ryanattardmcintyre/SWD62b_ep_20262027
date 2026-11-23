using Common.Models;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ReservationsRepository
    {
        LibraryDbContext _context;
        public ReservationsRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public IQueryable<Reservation> GetResevations()
        {
            return _context.Reservations;
        }
    }
}
