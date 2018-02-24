using EF.VenueBooking.Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static EF.VenueBooking.Domain.Seat;

namespace EF.VenueBooking.Tests.Domain
{
    public class SeatTests
    {
        [Fact]
        public void it_can_be_created_unreserved()
        {
            var seat = Seat.Unreserved(Guid.NewGuid(), 1);
            seat.IsReserved.Should().BeFalse();
        }

        [Fact]
        public void it_can_be_reserved_for_attendee()
        {
            var seat = Seat.Unreserved(Guid.NewGuid(), 1);
            seat.Reserve("fuubar");
            seat.IsReserved.Should().BeTrue();
        }


        [Fact]
        public void it_cannot_be_reserved_twice_of_course()
        {
            var seat = Seat.Unreserved(Guid.NewGuid(), 1);
            seat.Reserve("fuubar");

            
        }
    }
}
