using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventEase_Booking_System.Models;

namespace EventEase_Booking_System.Data
{
    public class EventEase_Booking_SystemContext : DbContext
    {
        public EventEase_Booking_SystemContext (DbContextOptions<EventEase_Booking_SystemContext> options)
            : base(options)
        {
        }

        public DbSet<EventEase_Booking_System.Models.Booking> Booking { get; set; } = default!;
        public DbSet<EventEase_Booking_System.Models.Event> Event { get; set; } = default!;
        public DbSet<EventEase_Booking_System.Models.Venue> Venue { get; set; } = default!;
    }
}
