using EF.VenueBooking.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.VenueBooking.Infrastructure.EntityFramework.Mappings
{
    public class SeatMap : IEntityTypeConfiguration<Seat>
    {

        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.HasKey(_ => new { _.VenueId, _.SeatNo });
        }
    }
}
