using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EF.VenueBooking.Domain
{
    public interface VenueRepository
    {
        Task<Unit> Add(Venue venue);

        Task<Option<Venue>> Get(Guid venueId);

        Task<Unit> Commit();
    }
}
