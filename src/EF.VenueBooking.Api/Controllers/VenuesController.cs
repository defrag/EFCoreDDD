using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF.VenueBooking.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EF.VenueBooking.Api.Controllers
{
    [Route("api/[controller]")]
    public class VenuesController : Controller
    {
        private readonly VenueRepository _repo;

        public VenuesController(VenueRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var id = Guid.NewGuid();
            var v = Venue.WithNumberOfSeats(id, new Location("Cracov", "Florianska 1"), 10);

            v.ReserveFor("foobar");
            await _repo.Add(v);

            var added = await _repo.Get(id);
            v.ReserveFor("barbaz");


            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
