using EF.VenueBooking.Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using EF.VenueBooking.Application.ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Linq;

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
            using (var connection = _context.Database.GetDbConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(@"
                    SELECT [VenueId], [Address], [City]
                    FROM [Venues] 
                    WHERE [VenueId] = @id", new { id });

                if (null == result)
                {
                    return null;
                }

                return new Venue(new Guid(result.VenueId), result.City, result.Address);
            }
        }
    }
}
