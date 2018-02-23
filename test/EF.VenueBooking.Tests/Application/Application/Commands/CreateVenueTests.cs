using EF.VenueBooking.Application.Commands;
using EF.VenueBooking.Application.Queries;
using EF.VenueBooking.Tests.Testing;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EF.VenueBooking.Tests.Application.Application.Commands
{
    public class CreateVenueTests 
    {
        [Fact]
        public async Task it_can_create_venue()
        {
            using (var ct = new CommandTest())
            {
                var id = Guid.NewGuid();
                var command = new CreateVenue(
                    id,
                    "Cracov",
                    "Florianska 10",
                    100,
                    new[] { ("CODE1", "IntelliJ"),("CODE2", "IntelliJ") }
                );

                // dispatch command
                await ct.Dispatch(command);
                
                // assert query side
                var queries = ct.GetService<VenueQueries>();
                var res = await queries.Find(id);
                res.Match(Some: _ =>
                {
                    _.Should().NotBeNull();
                    _.VenueId.Should().Be(id);
                    _.City.Should().Be("Cracov");
                    _.Address.Should().Be("Florianska 10");
                }, None: () => true.Should().BeFalse());
                
            }
        }
    }
}
