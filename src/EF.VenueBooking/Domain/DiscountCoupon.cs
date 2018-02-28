using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF.VenueBooking.Domain
{
    public sealed class DiscountCoupon
    {
        public string CouponCode { get; private set; }

        public string ProductName { get; private set; }

        private DiscountCoupon(string couponCode, string productName)
        {
            CouponCode = couponCode;
            ProductName = productName;
        }

        public static Either<VenueError, DiscountCoupon> CreateDiscountCoupon(string couponCode, string productName)
        {
            if (string.IsNullOrEmpty(couponCode))
            {
                return new VenueError("Coupon code cannot be empty.");
            }

            if (string.IsNullOrEmpty(productName))
            {
                return new VenueError("Product name cannot be empty.");
            }

            return new DiscountCoupon(couponCode, productName);
        }
    }
}
