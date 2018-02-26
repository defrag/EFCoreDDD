using static EF.VenueBooking.Domain.Venue;
using EF.VenueBooking.Domain;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using LanguageExt;

namespace EF.VenueBooking.Application.Commands
{
    public sealed class CreateVenueHandler : IAsyncRequestHandler<CreateVenue>
    {
        private readonly VenueRepository _repo;

        public CreateVenueHandler(VenueRepository repo)
        {
            _repo = repo;
        }

        public Task Handle(CreateVenue command)
            => CreateVenueWithNumberOfSeatsAndCoupons(
                    command.VenueId,
                    new Location(command.City, command.Address),
                    command.Seats,
                    command.DiscountCoupons.Select(_ => new DiscountCoupon(_.Item1, _.Item2)).ToList()
                )
                .MapAsync(Save);
        
        private Task<LanguageExt.Unit> Save(Venue v)
            => _repo.Add(v).ContinueWith(x => _repo.Commit()).Unwrap();
    }
}
