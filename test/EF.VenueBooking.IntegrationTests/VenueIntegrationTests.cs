using EF.VenueBooking.Domain;
using EF.VenueBooking.Infrastructure.EntityFramework;
using EF.VenueBooking.IntegrationTests.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Dapper;
using FluentAssertions;

namespace EF.VenueBooking.IntegrationTests
{
    public class VenueIntegrationTests : IntegrationTestCase
    {
        [Fact]
        public async Task test_venue_booking_in_integration()
        {
            var venues = _context.Venues;
            var repo = new EntityFrameworkVenueRepository(_context);

            var id = Guid.NewGuid();
            var venue = Venue.WithNumberOfSeatsAndCoupons(
                id,
                new Location("Cracov", "Florianska 1"),
                10,
                new List<DiscountCoupon>() { new DiscountCoupon("CODE0001", "IntelliJ"), new DiscountCoupon("CODE0002", "IntelliJ") }
            );

            await repo.Add(venue);

            var connString = _context.Database.GetDbConnection().ConnectionString;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(@"SELECT
                    [VenueId],
                    [AvailableCoupons],
                    [DispatchedCoupons],
                    [Address],
                    [City]
                    FROM [dbo].[Venues] 
                    WHERE [VenueId] = @id", new { id });


                Assert.Equal("Cracov", result.First().City);
                Assert.Equal("Florianska 1", result.First().Address);
                Assert.Equal("[]", result.First().DispatchedCoupons);
                Assert.Equal("[{\"CouponCode\":\"CODE0001\",\"ProductName\":\"IntelliJ\"},{\"CouponCode\":\"CODE0002\",\"ProductName\":\"IntelliJ\"}]", result.First().AvailableCoupons);
            }
        }
    }
}
