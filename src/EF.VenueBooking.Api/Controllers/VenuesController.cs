using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF.VenueBooking.Domain;

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
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
