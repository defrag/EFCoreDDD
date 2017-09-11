using Newtonsoft.Json;
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

        private List<DiscountCoupon> AvailableCoupons { get; set; }

        private List<(string, DiscountCoupon)> DispatchedCoupons { get; set; }

        private Venue()
        {

        }

        public Venue(Guid venueId, Location location, List<Seat> seats, List<DiscountCoupon> attendenceDiscountCoupons)
        {
            VenueId = venueId;
            Location = location ?? throw new ArgumentNullException(nameof(location));
            Seats = seats ?? throw new ArgumentNullException(nameof(seats));
            AvailableCoupons = attendenceDiscountCoupons ?? new List<DiscountCoupon>();
            DispatchedCoupons = new List<(string, DiscountCoupon)>();
        }

        public static Venue WithNumberOfSeats(Guid venueId, Location location, int numberOfSeats)
        {
            var seats = Enumerable
                .Range(1, numberOfSeats)
                .Select(id => Seat.Unreserved(venueId, id))
                .ToList();

            return new Venue(venueId, location, seats, new List<DiscountCoupon>());
        }

        public static Venue WithNumberOfSeatsAndCoupons(Guid venueId, Location location, int numberOfSeats, List<DiscountCoupon> coupons)
        {
            var seats = Enumerable
                .Range(1, numberOfSeats)
                .Select(id => Seat.Unreserved(venueId, id))
                .ToList();

            return new Venue(venueId, location, seats, coupons);
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

            if (HasAvailableCoupons)
            {
                var coupon = AvailableCoupons[0];
                AvailableCoupons.RemoveAt(0);
                DispatchedCoupons.Add((attendeeId, coupon));
            }
        }

        public bool HasCouponFor(string atteneeId) => DispatchedCoupons.Any(_ => _.Item1 == atteneeId);

        private bool AlreadyReservedFor(string attendeeId) => ReservedSeats.Any(_ => _.Attendee == attendeeId);

        private List<Seat> FreeSeats => Seats.Where(_ => false == _.IsReserved).ToList();

        private List<Seat> ReservedSeats => Seats.Where(_ => _.IsReserved).ToList();

        private bool FreeSeatsAvailable => FreeSeats.Count > 0;

        private bool HasAvailableCoupons => AvailableCoupons.Count > 0;

        internal string AvailableCouponsSerialized
        {
            get { return JsonConvert.SerializeObject(AvailableCoupons); }
            private set { AvailableCoupons = JsonConvert.DeserializeObject<List<DiscountCoupon>>(value); }
        }

        internal string DispatchedCouponsSerialized
        {
            get { return JsonConvert.SerializeObject(DispatchedCoupons); }
            private set { DispatchedCoupons = JsonConvert.DeserializeObject<List<(string, DiscountCoupon)>>(value); }
        }
    }
}
