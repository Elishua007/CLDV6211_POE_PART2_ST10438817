using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase_Booking_System.Models
{
    public class Booking
    {
        public int? BookingID { get; set; }
        public DateTime? BookingStartDate { get; set; }
        public DateTime? BookingEndDate { get; set; }
        public int? EventID { get; set; }
        [ForeignKey("EventID")]
        
        public int? VenueID { get; set; }
        [ForeignKey("VenueID")]

        public Venue? Venue { get; set; }
        public Event? Event { get; set; }
        
    }
}
