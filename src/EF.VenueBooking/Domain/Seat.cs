using LanguageExt;
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

        public Either<SeatError, Unit> Reserve(string attendee)
        {
            if (IsReserved)
            {
                return new SeatPreviouslyReserved($"Seat was already reserved for {Attendee}.");
            }
            Attendee = attendee;

            return new Unit();
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
