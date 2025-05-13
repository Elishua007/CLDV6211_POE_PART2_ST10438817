using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase_Booking_System.Models
{
    public class Booking
    {
        public int? BookingID { get; set; }

        [Required(ErrorMessage = "Booking Start Date is required")]
        public DateTime? BookingStartDate { get; set; }

        [Required(ErrorMessage = "Booking End Date is required")]
        public DateTime? BookingEndDate { get; set; }

        [Required(ErrorMessage = "Event ID is required")]
        public int? EventID { get; set; }
        [ForeignKey("EventID")]

        [Required(ErrorMessage = "Venue ID is required")]
        public int? VenueID { get; set; }
        [ForeignKey("VenueID")]

        public Venue? Venue { get; set; }
        public Event? Event { get; set; }
        
    }
}
