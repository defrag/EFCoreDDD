using EF.VenueBooking.Domain;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace EF.VenueBooking.Infrastructure.EntityFramework
{
    public sealed class EntityFrameworkVenueRepository : VenueRepository
    {
        private readonly VenueBookingContext _context;

        public EntityFrameworkVenueRepository(VenueBookingContext context)
        {
            _context = context;
        }

        public async Task<Unit> Add(Venue venue)
        {
            await _context.AddAsync(venue);
            await _context.SaveChangesAsync();

            return unit;
        }

        public async Task<Venue> Get(Guid venueId)
        {
            return await _context.Venues.SingleOrDefaultAsync(_ => _.VenueId == venueId);
        }
    }
}
