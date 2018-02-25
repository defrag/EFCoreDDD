using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF.VenueBooking.Application.Commands
{
    public sealed class RegisterAttendeeToVenue : IRequest
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
