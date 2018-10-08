using EF.VenueBooking.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EF.VenueBooking.IntegrationTests.Testing
{
    public class IntegrationTestCase : IDisposable
    {
        protected VenueBookingContext _context;

        public IntegrationTestCase()
        {
            var serviceProvider = new ServiceCollection()
               .AddEntityFrameworkSqlServer()
               .BuildServiceProvider();
            var builder = new DbContextOptionsBuilder<VenueBookingContext>();
            builder
                .UseInMemoryDatabase("testdb")
                .UseInternalServiceProvider(serviceProvider);
                
            _context = new VenueBookingContext(builder.Options);
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
