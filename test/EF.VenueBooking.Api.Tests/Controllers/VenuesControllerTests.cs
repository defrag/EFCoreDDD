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
                var content = new StringContent(payload, Encoding.UTF8, "text/json");
                var response = await af.Client.PostAsync("api/venues", content);
                response.StatusCode.Should().Be(HttpStatusCode.Created);
            }
        }
    }
}
