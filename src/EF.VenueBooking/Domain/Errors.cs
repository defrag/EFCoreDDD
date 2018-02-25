using LanguageExt;
using System;

namespace EF.VenueBooking.Domain
{
    public class VenueError
    {
        public VenueError(string value) 
        {
            Value = value;
        }

        public string Value { get; }
    }

    public class SeatAlreadyReservedForAttendee : VenueError
    {
        public SeatAlreadyReservedForAttendee(string message) : base(message)
        {
        }
    }

    public class NoMoreSeatsAvailable : VenueError
    {
        public NoMoreSeatsAvailable(string message) : base(message)
        {
        }
    }

    public class VenueNotFound : VenueError
    {
        public VenueNotFound(string message) : base(message)
        {
        }
    }

    public class SeatPreviouslyReserved : VenueError
    {
        public SeatPreviouslyReserved(string value) : base(value)
        {
        }
    }
}