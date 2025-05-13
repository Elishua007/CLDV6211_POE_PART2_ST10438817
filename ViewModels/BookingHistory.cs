using EventEase_Booking_System.Models;

namespace EventEase_Booking_System.ViewModels
{
    public class BookingHistory
    {

        public List<Booking>? Bookings { get; set; }

        public string? FilterVenueName { get; set; }
        public string? FilterEventName { get; set; }
        public DateTime? FilterBookingStartDate { get; set; }
        public DateTime? FilterBookingEndDate { get; set; }

    }
}
