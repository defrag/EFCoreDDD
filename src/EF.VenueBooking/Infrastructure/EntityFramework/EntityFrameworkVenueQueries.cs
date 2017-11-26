using EF.VenueBooking.Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using EF.VenueBooking.Application.ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EF.VenueBooking.Infrastructure.EntityFramework
{
    public sealed class EntityFrameworkVenueQueries : VenueQueries
    {
        private readonly VenueBookingContext _context;

        public EntityFrameworkVenueQueries(VenueBookingContext context)
        {
            _context = context;
        }

        public async Task<Venue> Find(Guid id)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT VenueId, City, Address From Venue WHERE VenueId = " +id.ToString();
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    var res = await result.ReadAsync();
                    if (!res)
                    {
                        return null;
                    }

                    return new Venue(result.GetGuid(0), result.GetString(1), result.GetString(2));
                }
            }
        }
    }
}
