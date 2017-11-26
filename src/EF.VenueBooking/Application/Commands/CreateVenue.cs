using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF.VenueBooking.Application.Commands
{
    public sealed class CreateVenue : IRequest
    {
        public CreateVenue(Guid venueId, string city, string address, int seats, IEnumerable<Tuple<string, string>> discountCoupons)
        {
            VenueId = venueId;
            City = city;
            Address = address;
            Seats = seats;
            DiscountCoupons = discountCoupons;
        }

        public Guid VenueId { get; }
        public string City { get; }
        public string Address { get; }
        public int Seats { get; }
        public IEnumerable<Tuple<string, string>> DiscountCoupons { get; }
    }
}
