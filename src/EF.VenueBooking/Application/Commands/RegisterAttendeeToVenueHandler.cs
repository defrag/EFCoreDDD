using EF.VenueBooking.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;
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
           await 
                venue.Match(
                    Some: (x) => Right<VenueError, Venue>(x),
                    None: () => Left<VenueError, Venue>(new VenueError("Venue not found."))
                )
                .Bind(_ => _.ReserveFor(message.AttendeeId))
                .MapAsync(_ => _repo.Commit());
            ;
        }

        private Either<VenueError, Venue> Reserve(Venue v, string attendeeId)
            => v.ReserveFor(attendeeId);
    }
}
