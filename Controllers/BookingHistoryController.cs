using EventEase_Booking_System.Data;
using EventEase_Booking_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase_Booking_System.Controllers
{
    public class BookingHistoryController : Controller
    {
        private readonly EventEase_Booking_SystemContext _context;

        public BookingHistoryController(EventEase_Booking_SystemContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var model = new BookingHistory
            {
                Bookings = await _context.Booking
           .Include(b => b.Event)
           .Include(b => b.Venue)
           .ToListAsync()
            };
            return View(model);
        }

        public async Task<IActionResult> BookingHistory(BookingHistory model) 
        {
            // Simulate fetching booking history from a database
            var bookings = _context.Booking.Include(e => e.Event).Include(e => e.Venue).AsQueryable();
            if (!string.IsNullOrEmpty(model.FilterEventName))
            {
                bookings = bookings.Where(e => e.Event.EventName.Contains(model.FilterEventName));
            }

            if (!string.IsNullOrEmpty(model.FilterVenueName))
            {
                bookings = bookings.Where(e => e.Venue.VenueName.Contains(model.FilterVenueName));
            }

            if (model.FilterBookingStartDate.HasValue)
            {
                bookings = bookings.Where(e => e.BookingStartDate >= model.FilterBookingStartDate.Value);
            }

            if (model.FilterBookingEndDate.HasValue)
            {
                bookings = bookings.Where(e => e.BookingEndDate <= model.FilterBookingEndDate.Value);
            }


           
            model.Bookings = await bookings.ToListAsync();

            
            return View("Index", model);

        }

    }
}
