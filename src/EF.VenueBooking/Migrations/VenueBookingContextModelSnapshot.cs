﻿// <auto-generated />
using EF.VenueBooking.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EF.VenueBooking.Migrations
{
    [DbContext(typeof(VenueBookingContext))]
    partial class VenueBookingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EF.VenueBooking.Domain.Seat", b =>
                {
                    b.Property<Guid>("VenueId");

                    b.Property<int>("SeatNo");

                    b.Property<string>("Attendee");

                    b.HasKey("VenueId", "SeatNo");

                    b.ToTable("Seat");
                });

            modelBuilder.Entity("EF.VenueBooking.Domain.Venue", b =>
                {
                    b.Property<Guid>("VenueId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvailableCouponsSerialized")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AvailableCoupons")
                        .HasDefaultValue("[]");

                    b.Property<string>("DispatchedCouponsSerialized")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DispatchedCoupons")
                        .HasDefaultValue("[]");

                    b.HasKey("VenueId");

                    b.ToTable("Venues");
                });

            modelBuilder.Entity("EF.VenueBooking.Domain.Seat", b =>
                {
                    b.HasOne("EF.VenueBooking.Domain.Venue")
                        .WithMany("Seats")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EF.VenueBooking.Domain.Venue", b =>
                {
                    b.OwnsOne("EF.VenueBooking.Domain.Location", "Location", b1 =>
                        {
                            b1.Property<Guid>("VenueId");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnName("Address");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnName("City");

                            b1.ToTable("Venues");

                            b1.HasOne("EF.VenueBooking.Domain.Venue")
                                .WithOne("Location")
                                .HasForeignKey("EF.VenueBooking.Domain.Location", "VenueId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
