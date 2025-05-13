using System.ComponentModel.DataAnnotations;

namespace EventEase_Booking_System.Models
{
    public class Venue
    {
        public int? VenueID { get; set; }

        [Required(ErrorMessage = "Venue Name is required")]
        public string? VenueName { get; set; }

        [Required(ErrorMessage = "Venue Location is required")]
        public string? VenueLocation { get; set; }

        [Required(ErrorMessage = "Venue Capacity is required")]
        public string? VenueCapacity { get; set; }
        public string? VenueURL { get; set; }
    }
}
