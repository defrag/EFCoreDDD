using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EF.VenueBooking.Api.Renditions
{
    public sealed class CreateVenueRendition
    {
        [Required]
        public string City { set; get; }

        [Required]
        public string Address { set; get; }

        [Required]
        public int Seats { set; get; }

        public List<CreateVenueDiscountCouponRendition> DiscountCoupons { set; get; }
    }

    public sealed class CreateVenueDiscountCouponRendition
    {
        [Required]
        public string CouponCode { set; get; }

        [Required]
        public string ProductName { set; get; }
    }
}
