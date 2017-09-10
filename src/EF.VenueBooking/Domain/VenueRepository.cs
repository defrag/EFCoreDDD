using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EF.VenueBooking.Domain
{
    public interface VenueRepository
    {
        Task Add(Venue venue);

        Task<Venue> Get(Guid venueId);
    }
}
