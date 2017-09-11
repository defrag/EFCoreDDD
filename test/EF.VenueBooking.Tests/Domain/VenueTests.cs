﻿using EF.VenueBooking.Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EF.VenueBooking.Tests.Domain
{
    public class VenueTests
    {
        [Fact]
        public void it_is_created_with_valid_location_and_number_of_seats()
        {
            var venue = Venue.WithNumberOfSeats(Guid.NewGuid(), new Location("Cracov", "Florianska 1"), 10);
        }

        [Fact]
        public void it_can_reserve_seats_for_attendee_once()
        {
            var venue = Venue.WithNumberOfSeats(Guid.NewGuid(), new Location("Cracov", "Florianska 1"), 10);
            venue.ReserveFor("neo");

            venue.Invoking(v => v.ReserveFor("neo"))
                .ShouldThrow<SeatAlreadyReservedForAttendee>()
                .WithMessage("Seat already reserved for neo.");
        }

        [Fact]
        public void it_cannot_reserve_seats_if_no_more_are_available()
        {
            var venue = Venue.WithNumberOfSeats(Guid.NewGuid(), new Location("Cracov", "Florianska 1"), 1);
            venue.ReserveFor("neo");

            venue.Invoking(v => v.ReserveFor("michal"))
                .ShouldThrow<NoMoreSeatsAvailable>()
                .WithMessage("No more seats available for this venue.");
        }

        [Fact]
        public void it_registers_coupons_for_first_attendees_if_coupons_are_provided()
        {
            var venue = Venue.WithNumberOfSeatsAndCoupons(
                Guid.NewGuid(), 
                new Location("Cracov", "Florianska 1"), 
                10,
                new List<DiscountCoupon>() { new DiscountCoupon("CODE1", "Visual Studio Enterprice") }
            );
            venue.ReserveFor("neo");
            venue.HasCouponFor("neo").Should().BeTrue();
        }

        [Fact]
        public void it_doesnt_dispatch_any_more_coupons_if_all_were_dispatched()
        {
            var venue = Venue.WithNumberOfSeatsAndCoupons(
                Guid.NewGuid(),
                new Location("Cracov", "Florianska 1"),
                10,
                new List<DiscountCoupon>() { new DiscountCoupon("CODE1", "Visual Studio Enterprice") }
            );
            venue.ReserveFor("neo");
            venue.ReserveFor("michi");
            venue.HasCouponFor("michi").Should().BeFalse();
        }

    }
}