using EF.VenueBooking.Api.Tests.Testing;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EF.VenueBooking.Api.Tests.Controllers
{
    public class VenuesControllerTests
    {
        [Fact]
        public async Task it_responds_to_create_request()
        {
            using (var af = new ApiFixture())
            {
                var payload = @"
                    {
                      ""City"": ""Cracov"",
                      ""Address"": ""Florianska 1"",
                      ""Seats"": ""10"",
                      ""DiscountCoupons"": [
                          {""CouponCode"": ""PC0001"", ""ProductName"": ""Awesome PC"" },
                          {""CouponCode"": ""PC0002"", ""ProductName"": ""Awesome PC #2"" }
                      ]
                    }
                ";
                var response = await af.PostJson("api/venues", payload);
                response.StatusCode.Should().Be(HttpStatusCode.Created);

                var responseBody = await response.Content.ReadAsStringAsync();

                responseBody.Should().MatchJson(@"{""venueId"":""@guid@"",""city"":""Cracov"",""address"":""Florianska 1""}");
            }
        }

        [Fact]
        public async Task it_responds_with_404_if_venue_doesnt_exist()
        {
            using (var af = new ApiFixture())
            {
                var guid = Guid.NewGuid();
                var response = await af.Client.GetAsync($"api/venues/{guid}");
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        [Fact]
        public async Task it_responds_with_200_if_venue_exists()
        {
            using (var af = new ApiFixture())
            {
                var id = Guid.NewGuid();
                await af.State.CreateVenue(id, "Warsaw", "St 1");
                var response = await af.Client.GetAsync($"api/venues/{id}");
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var responseBody = await response.Content.ReadAsStringAsync();
                responseBody.Should().MatchJson(@"{""venueId"":""@guid@"",""city"":""Warsaw"",""address"":""St 1""}");
            }
        }

        [Fact]
        public async Task it_responds_with_204_when_registering_succesfully_to_venue()
        {
            using (var af = new ApiFixture())
            {
                var id = Guid.NewGuid();
                await af.State.CreateVenue(id, "Warsaw", "St 1");

                var payload = @"
                    {
                      ""AttendeeId"": ""michi""
                    }
                ";
                var response = await af.PostJson($"api/venues/{id}/register", payload);
                response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            }
        }
    }
}
