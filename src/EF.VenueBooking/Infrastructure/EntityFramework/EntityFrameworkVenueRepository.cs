using EF.VenueBooking.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.VenueBooking.Infrastructure.EntityFramework
{
    public sealed class EntityFrameworkVenueRepository : VenueRepository
    {
        private readonly VenueBookingContext _context;

        public EntityFrameworkVenueRepository(VenueBookingContext context)
        {
            _context = context;
        }

        public async Task Add(Venue venue)
        {
            await _context.AddAsync(venue);
            await _context.SaveChangesAsync();
        }

        public async Task<Venue> Get(Guid venueId)
        {
            return await _context.Venues.SingleOrDefaultAsync(_ => _.VenueId == venueId);
        }
    }
}
