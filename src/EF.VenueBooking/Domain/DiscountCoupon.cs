using System;
using System.Collections.Generic;
using System.Text;

namespace EF.VenueBooking.Domain
{
    public sealed class DiscountCoupon
    {
        public string CouponCode { get; private set; }

        public string ProductName { get; private set; }

        public DiscountCoupon(string couponCode, string productName)
        {
            CouponCode = couponCode ?? throw new ArgumentNullException(nameof(couponCode));
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
        }
    }
}
