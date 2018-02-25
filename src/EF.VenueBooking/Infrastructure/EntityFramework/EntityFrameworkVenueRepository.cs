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

            return unit;
        }

        public async Task<Option<Venue>> Get(Guid venueId)
        {
            var result = await _context
                .Venues
                .Include(_ => _.Seats)
                .SingleOrDefaultAsync(_ => _.VenueId == venueId);

            return result != null ? Some(result) : None;
        }

        public async Task<Unit> Commit()
        {
            await _context.SaveChangesAsync();

            return unit;
        }

    }
}
