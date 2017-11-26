using EF.VenueBooking.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EF.VenueBooking.Application.Queries
{
    public interface VenueQueries
    {
        Task<Venue> Find(Guid id);
    }
}
