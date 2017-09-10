using EF.VenueBooking.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF.VenueBooking.Infrastructure.EntityFramework
{
    public sealed class VenueBookingContext : DbContext
    {
        public VenueBookingContext(DbContextOptions<VenueBookingContext> options): base(options)
        {

        }
        public DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Mappings.VenueMap());
            modelBuilder.ApplyConfiguration(new Mappings.SeatMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
