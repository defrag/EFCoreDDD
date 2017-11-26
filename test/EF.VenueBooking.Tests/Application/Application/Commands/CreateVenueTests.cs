using EF.VenueBooking.Application.Commands;
using EF.VenueBooking.Application.Queries;
using EF.VenueBooking.Infrastructure.EntityFramework;
using EF.VenueBooking.Tests.Testing;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EF.VenueBooking.Tests.Application.Application.Commands
{
    public class CreateVenueTests : CommandTest
    {
        [Fact]
        public async Task it_can_create_venue()
        {
            var id = Guid.NewGuid();
            var command = new CreateVenue(
                id, 
                "Cracov", 
                "Florianska 10", 
                100, 
                new[] { Tuple.Create("CODE1", "IntelliJ"), Tuple.Create("CODE2", "IntelliJ") }
            );
            
            // dispatch command
            await Dispatch(command);


            // assert entry exists in context
            var context = GetService<VenueBookingContext>();

            var entry = context
                .Venues
                .Find(id);

            entry.VenueId.Should().Be(id);
        }
    }
}
