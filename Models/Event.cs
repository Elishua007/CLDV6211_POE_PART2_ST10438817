using System.ComponentModel.DataAnnotations;

namespace EventEase_Booking_System.Models
{
    public class Event
    {
        public int? EventID { get; set; }

        [Required(ErrorMessage = "Event Name is required")]
        public string? EventName { get; set; }

        [Required(ErrorMessage = "Event Description is required")]
        public string? EventDescription { get; set; }

    }
}
