using EF.VenueBooking.Domain;
using LanguageExt;
using MediatR;
using System.Threading.Tasks;

namespace EF.VenueBooking.Application.Commands
{
    public sealed class RegisterAttendeeToVenueHandler : IAsyncRequestHandler<RegisterAttendeeToVenue, Either<VenueError, LanguageExt.Unit>>
    {
        private readonly VenueRepository _repo;

        public RegisterAttendeeToVenueHandler(VenueRepository repo)
        {
            _repo = repo;
        }


        public async Task<Either<VenueError, LanguageExt.Unit>> Handle(RegisterAttendeeToVenue message)
        {
            var venue = await _repo.Get(message.VenueId);

            return await venue
                 .ToEither(new VenueError("Venue not found."))
                 .Bind(_ => _.ReserveFor(message.AttendeeId))
                 .MapAsync(_ => _repo.Commit());
            ;
        }
    }
}
