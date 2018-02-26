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

        public Task<Either<VenueError, LanguageExt.Unit>> Handle(RegisterAttendeeToVenue message)
            => _repo.Get(message.VenueId)
                .ToEitherAsync(new VenueNotFound("Venue not found.") as VenueError)
                .BindAsync(_ => _.ReserveFor(message.AttendeeId))
                .MapAsync(_ => _repo.Commit());
    }
}
