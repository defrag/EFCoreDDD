using EF.VenueBooking.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.VenueBooking.Application.Commands
{
    public sealed class CreateVenueHandler : IAsyncRequestHandler<CreateVenue>
    {
        private readonly VenueRepository _repo;

        public CreateVenueHandler(VenueRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(CreateVenue command)
        {
            var venue = Venue.WithNumberOfSeatsAndCoupons(
                command.VenueId,
                new Location(command.City, command.Address),
                command.Seats,
                command.DiscountCoupons.Select(_ => new DiscountCoupon(_.Item1, _.Item2)).ToList()
            );

            await _repo.Add(venue);
        }
    }
}
