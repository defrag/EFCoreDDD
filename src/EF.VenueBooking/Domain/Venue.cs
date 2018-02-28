using LanguageExt;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static LanguageExt.Prelude;

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

        private Venue(Guid venueId, Location location, List<Seat> seats, List<DiscountCoupon> attendenceDiscountCoupons)
        {
            VenueId = venueId;
            Location = location;
            Seats = seats;
            AvailableCoupons = attendenceDiscountCoupons;
            DispatchedCoupons = new List<(string, DiscountCoupon)>();
        }

        public static Either<VenueError, Venue> CreateVenueWithNumberOfSeats(Guid venueId, Location location, int numberOfSeats)
            => CreateVenueWithNumberOfSeatsAndCoupons(venueId, location, numberOfSeats, new List<DiscountCoupon>());

        public static Either<VenueError, Venue> CreateVenueWithNumberOfSeatsAndCoupons(Guid venueId, Location location, int numberOfSeats, List<DiscountCoupon> coupons)
        {
            if (numberOfSeats < 1)
            {
                return new VenueError("Number of seats need to be greater than zero.");
            }

            var seats = Enumerable
                .Range(1, numberOfSeats)
                .Select(id => Seat.CreateFreeSeat(venueId, id))
                .Sequence();

            return from s in seats
                   from venue in Apply(venueId, location, s.ToList(), coupons)
                   select venue;
        }

        public Either<VenueError, Venue> ReserveFor(string attendeeId)
        {
            if (AlreadyReservedFor(attendeeId))
            {
                return new SeatAlreadyReservedForAttendee($"Seat already reserved for {attendeeId}.");
            }

            if (false == FreeSeatsAvailable)
            {
                return new NoMoreSeatsAvailable("No more seats available for this venue.");
            }

            var seat = FreeSeats.First();

            return seat
                .Reserve(attendeeId)
                .Map(_ => DispatchCouponIfAvailable(attendeeId))
                .Map(_ => this);
        }

        public bool HasCouponFor(string atteneeId) => DispatchedCoupons.Any(_ => _.Item1 == atteneeId);

        private bool AlreadyReservedFor(string attendeeId) => ReservedSeats.Any(_ => _.Attendee == attendeeId);

        private List<Seat> FreeSeats => Seats.Where(_ => false == _.IsReserved).ToList();

        private List<Seat> ReservedSeats => Seats.Where(_ => _.IsReserved).ToList();

        private bool FreeSeatsAvailable => FreeSeats.Count > 0;

        private bool HasAvailableCoupons => AvailableCoupons.Count > 0;

        private Unit DispatchCouponIfAvailable(string attendeeId)
        {
            if (HasAvailableCoupons)
            {
                var coupon = AvailableCoupons[0];
                AvailableCoupons.RemoveAt(0);
                DispatchedCoupons.Add((attendeeId, coupon));
            }

            return unit;
        }

        private static Either<VenueError, Venue> Apply(Guid venueId, Location location, List<Seat> seats, List<DiscountCoupon> attendenceDiscountCoupons)
        {
            if (location == null)
            {
                return new VenueError("Venue needs to have a location.");
            }

            if (seats == null)
            {
                return new VenueError("Venue needs to have seats.");
            }

            if (attendenceDiscountCoupons == null)
            {
                return new VenueError("Venue attendence discount coupons cannot be null.");
            }

            return new Venue(venueId, location, seats, attendenceDiscountCoupons);
        }

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
