using System;
using System.Collections.Generic;
using System.Text;

namespace EF.VenueBooking.Domain
{
    public sealed class Seat
    {
        public int SeatNo { get; private set; }

        public Guid VenueId { get; private set; }

        public string Attendee { get; private set; }

        public static Seat Unreserved(Guid venueId, int seatNo)
        {
            return new Seat(venueId, seatNo);
        }

        public void Reserve(string attendee)
        {
            if (IsReserved)
            {
                throw new Exception("Seat already reserved");
            }
            Attendee = attendee;
        }

        public bool IsReserved => null != Attendee;

        private Seat()
        {

        }

        private Seat(Guid venueId, int seatNo)
        {
            VenueId = venueId;
            SeatNo = seatNo;
        }
    }
}
