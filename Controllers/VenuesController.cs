using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventEase_Booking_System.Data;
using EventEase_Booking_System.Models;
using Azure.Storage.Blobs;
using EventEase_Booking_System.AzureBlobStorage;

namespace EventEase_Booking_System.Controllers
{
    public class VenuesController : Controller
    {
        private readonly EventEase_Booking_SystemContext _context;
        //private readonly AzureBlobStorage _blobService;

        private readonly IBlobService _blob;


        public VenuesController(EventEase_Booking_SystemContext context, IBlobService blob)
        {
            _context = context;
            _blob = blob;
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            return View(await _context.Venue.ToListAsync());
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venue
                .FirstOrDefaultAsync(m => m.VenueID == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // GET: Venues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueID,VenueName,VenueLocation,VenueCapacity")] Venue venue, IFormFile ImageVenue)
        {
            if (ModelState.IsValid)
            {
                if (ImageVenue != null && ImageVenue.Length > 0)
                {
                    string imageUrl = await _blob.UploadFileAsync(ImageVenue.OpenReadStream(), ImageVenue.FileName);
                    venue.VenueURL = imageUrl; 
                }
                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venue.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }
            return View(venue);
        }

        // POST: Venues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("VenueID,VenueName,VenueLocation,VenueCapacity")] Venue venue, IFormFile ImageVenue)
        {
            if (id != venue.VenueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                var existingVenue = await _context.Venue.AsNoTracking().FirstOrDefaultAsync(v => v.VenueID == id);

                if (existingVenue == null)
                {
                    return NotFound();
                }

                if (ImageVenue != null && ImageVenue.Length > 0)
                {
                    if (!string.IsNullOrEmpty(existingVenue.VenueURL))
                    {
                        var fileName = Path.GetFileName(new Uri(existingVenue.VenueURL).LocalPath);
                        await _blob.DeleteFileAsync(fileName);
                    }

                    string imageUrl = await _blob.UploadFileAsync(ImageVenue.OpenReadStream(), ImageVenue.FileName);
                    venue.VenueURL = imageUrl;
                }
                else
                {
                    
                    venue.VenueURL = existingVenue.VenueURL;
                }

                try
                {
                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.VenueID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(venue);
        }


        // GET: Venues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venue
                .FirstOrDefaultAsync(m => m.VenueID == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            bool hasBooking = await _context.Booking.AnyAsync(b => b.VenueID == id);
            if (hasBooking)
            {
                var venues = await _context.Venue.FindAsync(id);
                ModelState.AddModelError("", "Cannot delete venue with existing bookings.");
                return View(venues);
            }
            else
            {
                var venue = await _context.Venue.FindAsync(id);
                if (venue != null)
                {
                    // 🔽 Delete blob from Azure before removing venue
                    if (!string.IsNullOrEmpty(venue.VenueURL))
                    {
                        var fileName = Path.GetFileName(new Uri(venue.VenueURL).LocalPath);
                        await _blob.DeleteFileAsync(fileName);
                    }

                    _context.Venue.Remove(venue);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool VenueExists(int? id)
        {
            return _context.Venue.Any(e => e.VenueID == id);
        }
    }
}
