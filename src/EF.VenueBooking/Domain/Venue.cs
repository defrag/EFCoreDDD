using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.VenueBooking.Domain
{
    public sealed class Venue
    {
        public Guid VenueId { get; private set; }

        public Location Location { get; private set; }

        public List<Seat> Seats { get; private set; }

        private Venue()
        {

        }

        public Venue(Guid venueId, Location location, List<Seat> seats)
        {
            VenueId = venueId;
            Location = location ?? throw new ArgumentNullException(nameof(location));
            Seats = seats ?? throw new ArgumentNullException(nameof(seats));
        }

        public static Venue WithNumberOfSeats(Guid venueId, Location location, int numberOfSeats)
        {
            var seats = Enumerable
                .Range(1, numberOfSeats)
                .Select(id => Seat.Unreserved(venueId, id))
                .ToList();
            return new Venue(venueId, location, seats);
        }

        public void ReserveFor(string attendeeId)
        {
            if (AlreadyReservedFor(attendeeId))
            {
                throw new SeatAlreadyReservedForAttendee($"Seat already reserved for {attendeeId}.");
            }

            if (false == FreeSeatsAvailable)
            {
                throw new NoMoreSeatsAvailable("No more seats available for this venue.");
            }
            FreeSeats.First().Reserve(attendeeId);
        }

        private bool AlreadyReservedFor(string attendeeId) => ReservedSeats.Any(_ => _.Attendee == attendeeId);

        private List<Seat> FreeSeats => Seats.Where(_ => false == _.IsReserved).ToList();

        private List<Seat> ReservedSeats => Seats.Where(_ => _.IsReserved).ToList();


        private bool FreeSeatsAvailable => FreeSeats.Count > 0;


    }
}
