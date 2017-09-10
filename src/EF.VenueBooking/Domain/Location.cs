using System;
using System.Collections.Generic;
using System.Text;

namespace EF.VenueBooking.Domain
{
    public sealed class Location
    {
        public string City { get; private set; }

        public string Address { get; private set; }

        private Location()
        {

        }

        public Location(string city, string address)
        {
            City = city ?? throw new ArgumentNullException(nameof(city));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public override string ToString()
        {
            return Address + ", " + City;
        }
    }
}
