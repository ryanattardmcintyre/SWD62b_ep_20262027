using Microsoft.AspNetCore.Mvc;
using Common.Models;
using DataAccess.Repositories;
using WebApplication1.BookingPolicies;
using Microsoft.AspNetCore.Authorization;
namespace WebApplication1.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private ReservationsRepository _reservationsRepository;
        private IBookingPolicy _winterPolicy;
        private IBookingPolicy _summerPolicy;

        //to note: here we are injecting two different implementations
        //         of the same interface together, side-by-side. i.e.
        //         we are NOT choosing between winter or summer, we're injecting
        //         them both UNLIKE BooksJsonRepo vs BooksRepo
        public ReservationsController(ReservationsRepository rr,
            [FromKeyedServices("winter")] IBookingPolicy winterPolicy,
            [FromKeyedServices("summer")] IBookingPolicy summerPolicy
            ) {
            _winterPolicy = winterPolicy;
            _summerPolicy = summerPolicy;
            _reservationsRepository = rr;
        } 
        public IActionResult Index()
        {
            var userReservations = _reservationsRepository.GetResevations().
                                    Where(x => x.Username == User.Identity.Name);

            return View(userReservations);
        }

       //One method to add a reservation in the database is enough as long as
       // the only input needed i.e. bookId is strongly-embedded inside a button
        public IActionResult Create(string bookId)
        {
            string username = User.Identity.Name;

            Reservation r = new Reservation();
            r.BookFK = bookId;
            r.Username = username;
            r.DateTaken = DateTime.Now;
            if (r.DateTaken.Month >= 6 && r.DateTaken.Month <= 9)
                r.DateToReturn = _summerPolicy.CalculateDate(r.DateTaken);
            else r.DateToReturn = _winterPolicy.CalculateDate(r.DateTaken);

                _reservationsRepository.AddReservation(r);

            TempData["message"] = "Reservation saved. You have to return the book by "+ r.DateToReturn.ToShortDateString();
            return RedirectToAction("Index");

        }
    }
}
