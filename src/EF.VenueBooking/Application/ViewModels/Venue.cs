using System;
using System.Collections.Generic;
using System.Text;

namespace EF.VenueBooking.Application.ViewModels
{
    public sealed class Venue
    {
        public Venue(Guid venueId, string city, string address)
        {
            VenueId = venueId;
            City = city;
            Address = address;
        }

        public Guid VenueId { get; }
        public string City { get; }
        public string Address { get; }
    }
}
