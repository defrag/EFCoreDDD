using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EF.VenueBooking.Api.Renditions
{
    public class RegisterAttendeeToVenueRendition
    {
        [Required]
        public string AttendeeId { set; get; }
    }
}
