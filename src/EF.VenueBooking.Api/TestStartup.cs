using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VenueBookingTestStartup = EF.VenueBooking.TestStartup;
using Microsoft.Extensions.Configuration;

namespace EF.VenueBooking.Api
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureVenueBooking(IServiceCollection services)
        {
            var vb = new VenueBookingTestStartup();
            vb.ConfigureServices(services, Configuration);
        }
    }
}
