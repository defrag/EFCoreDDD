using System;

namespace EF.VenueBooking.Domain
{
    public class VenueBookingException : Exception
    {
        public VenueBookingException(string message) : base(message)
        {
        }
    }

    public class SeatAlreadyReservedForAttendee : VenueBookingException
    {

        public SeatAlreadyReservedForAttendee(string message) : base(message)
        {
        }
    }

    public class NoMoreSeatsAvailable : VenueBookingException
    {

        public NoMoreSeatsAvailable(string message) : base(message)
        {
        }
    }
}