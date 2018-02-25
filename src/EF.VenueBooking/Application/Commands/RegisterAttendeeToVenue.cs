using EF.VenueBooking.Domain;
using LanguageExt;
using MediatR;
using System;

namespace EF.VenueBooking.Application.Commands
{
    public sealed class RegisterAttendeeToVenue : IRequest<Either<VenueError, LanguageExt.Unit>>
    {
        public RegisterAttendeeToVenue(Guid venueId, string attendeeId)
        {
            VenueId = venueId;
            AttendeeId = attendeeId;
        }

        public Guid VenueId { get; }
        public string AttendeeId { get; }
    }
}
