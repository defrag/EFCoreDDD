using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EF.VenueBooking.Domain;

namespace EF.VenueBooking.Infrastructure.EntityFramework.Mappings
{
    public class VenueMap : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.HasKey(_ => _.VenueId);
            builder.Property(b => b.AvailableCouponsSerialized).HasColumnName("AvailableCoupons").IsRequired().HasDefaultValue("[]");
            builder.Property(b => b.DispatchedCouponsSerialized).HasColumnName("DispatchedCoupons").IsRequired().HasDefaultValue("[]");

            builder.OwnsOne(_ => _.Location).Property(l => l.City).HasColumnName("City").IsRequired();
            builder.OwnsOne(_ => _.Location).Property(l => l.Address).HasColumnName("Address").IsRequired();
        }
    }
}
