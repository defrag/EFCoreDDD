using System;
using System.Collections.Generic;
using System.Text;

namespace EF.VenueBooking.Domain
{
    public sealed class VenueId
    {
        public Guid Identifier;

        private VenueId()
        {

        }

        public VenueId(Guid id)
        {
            Identifier = id;
        }

        public static VenueId Generate() => new VenueId(Guid.NewGuid());

        public override string ToString() => Identifier.ToString();

    }
}
