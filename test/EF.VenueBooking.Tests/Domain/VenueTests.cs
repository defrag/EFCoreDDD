using EF.VenueBooking.Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static EF.VenueBooking.Domain.Venue;

namespace EF.VenueBooking.Tests.Domain
{
    public class VenueTests
    {
        [Fact]
        public void it_cannot_be_created_without_location()
        {
            CreateVenueWithNumberOfSeats(Guid.NewGuid(), null, 10)
                .Match(
                    Right: (v) => true.Should().BeFalse(),
                    Left: (e) => {
                        e.Value.Should().Be("Venue needs to have a location.");
                    }
                );
        }

        [Fact]
        public void it_cannot_be_created_with_seats_less_than_one()
        {
            CreateVenueWithNumberOfSeats(Guid.NewGuid(), new Location("Cracov", "Florianska 1"), 0)
                .Match(
                    Right: (v) => true.Should().BeFalse(),
                    Left: (e) => {
                        e.Value.Should().Be("Number of seats need to be greater than zero.");
                    }
                );
        }

        [Fact]
        public void it_is_created_with_valid_location_and_number_of_seats()
        {
            var venue = CreateVenueWithNumberOfSeats(Guid.NewGuid(), new Location("Cracov", "Florianska 1"), 10);
            venue.IsRight.Should().BeTrue();
        }

        [Fact]
        public void it_can_reserve_seats_for_attendee_once()
        {
            CreateVenueWithNumberOfSeats(Guid.NewGuid(), new Location("Cracov", "Florianska 1"), 10)
                .Bind(_ => _.ReserveFor("neo"))
                .Bind(_ => _.ReserveFor("neo"))
                .Match(
                    Right: (v) => true.Should().BeFalse(),
                    Left: (e) => {
                        e.Should().BeAssignableTo<SeatAlreadyReservedForAttendee>();
                        e.Value.Should().Be("Seat already reserved for neo.");
                    }
                );
        }

        [Fact]
        public void it_cannot_reserve_seats_if_no_more_are_available()
        {
            CreateVenueWithNumberOfSeats(Guid.NewGuid(), new Location("Cracov", "Florianska 1"), 1)
                .Bind(_ => _.ReserveFor("michi"))
                .Bind(_ => _.ReserveFor("neo"))
                .Match(
                   Right: (v) => true.Should().BeFalse(),
                   Left: (e) => {
                       e.Should().BeAssignableTo<NoMoreSeatsAvailable>();
                       e.Value.Should().Be("No more seats available for this venue.");
                   }
                );
        }

        [Fact]
        public void it_registers_coupons_for_first_attendees_if_coupons_are_provided()
        {
            var venue = CreateVenueWithNumberOfSeatsAndCoupons(
                Guid.NewGuid(), 
                new Location("Cracov", "Florianska 1"), 
                10,
                new List<DiscountCoupon>() { new DiscountCoupon("CODE1", "Visual Studio Enterprice") }
            );
            venue
                .Bind(_ => _.ReserveFor("neo"))
                .Map(_ => _.HasCouponFor("neo"))
                .Match(
                    Right: (v) => v.Should().BeTrue(),
                    Left: (e) => true.Should().BeFalse()
                );
        }

        [Fact]
        public void it_doesnt_dispatch_any_more_coupons_if_all_were_dispatched()
        {
            var venue = CreateVenueWithNumberOfSeatsAndCoupons(
                Guid.NewGuid(),
                new Location("Cracov", "Florianska 1"),
                10,
                new List<DiscountCoupon>() { new DiscountCoupon("CODE1", "Visual Studio Enterprice") }
            );

            venue
                .Bind(_ => _.ReserveFor("neo"))
                .Bind(_ => _.ReserveFor("michi"))
                .Map(_ => _.HasCouponFor("michi"))
                .Match(
                    Right: (v) => v.Should().BeFalse(),
                    Left: (e) => true.Should().BeFalse()
                );
        }

    }
}
