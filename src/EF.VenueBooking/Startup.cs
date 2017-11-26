using EF.VenueBooking.Domain;
using EF.VenueBooking.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EF.VenueBooking
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VenueBookingContext>(GetDbOptions(configuration));
            services.AddTransient<VenueRepository, EntityFrameworkVenueRepository>();

            services.AddMediatR(GetType().GetTypeInfo().Assembly);
        }


        protected virtual Action<DbContextOptionsBuilder> GetDbOptions(IConfiguration configuration)
        {
            return options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("EF.VenueBooking.Database"));
        }
    }
}
