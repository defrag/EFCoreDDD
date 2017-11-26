using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF.VenueBooking
{
    public class TestStartup : Startup
    {
        protected override Action<DbContextOptionsBuilder> GetDbOptions(IConfiguration configuration)
        {
            return options => options.UseInMemoryDatabase($"InMemoryVenueDb-{Guid.NewGuid()}");
        }
    }
}
