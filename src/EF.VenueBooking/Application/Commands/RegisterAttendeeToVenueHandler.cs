using EF.VenueBooking.Domain;
using MediatR;
using System.Threading.Tasks;
using static EF.VenueBooking.Domain.Venue;

namespace EF.VenueBooking.Application.Commands
{
    public sealed class RegisterAttendeeToVenueHandler : IAsyncRequestHandler<RegisterAttendeeToVenue>
    {
        private readonly VenueRepository _repo;

        public RegisterAttendeeToVenueHandler(VenueRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(RegisterAttendeeToVenue message)
        {
           var venue = await _repo.Get(message.VenueId);
           await venue
                .ToEither(new VenueError("Venue not found."))
                .Bind(_ => _.ReserveFor(message.AttendeeId))
                .MapAsync(_ => _repo.Commit());
            ;
        }
    }
}
