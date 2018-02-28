using EF.VenueBooking.Domain;
using static EF.VenueBooking.Domain.DiscountCoupon;
using static EF.VenueBooking.Domain.Venue;
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
using LanguageExt;

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
            var coupons = new[] { CreateDiscountCoupon("CODE0001", "IntelliJ"), CreateDiscountCoupon("CODE0002", "IntelliJ") };

            await coupons
                .Sequence()
                .Bind(c =>
                    CreateVenueWithNumberOfSeatsAndCoupons(
                        id,
                        new Location("Cracov", "Florianska 1"),
                        10,
                        c.ToList()
                    )
                )
                .MapAsync(v => repo.Add(v))
                .MapAsync(_ => repo.Commit());

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
