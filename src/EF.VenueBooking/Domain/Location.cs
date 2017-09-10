using System;
using System.Collections.Generic;
using System.Text;

namespace EF.VenueBooking.Domain
{
    public sealed class Location
    {
        private string _city;

        private string _address;

        private Location()
        {

        }

        public Location(string city, string address)
        {
            _city = city ?? throw new ArgumentNullException(nameof(city));
            _address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public override string ToString()
        {
            return _address + ", " + _city;
        }
    }
}
