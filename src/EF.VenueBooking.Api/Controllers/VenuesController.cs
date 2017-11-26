using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF.VenueBooking.Domain;
using EF.VenueBooking.Api.Renditions;
using EF.VenueBooking.Application.Commands;
using MediatR;
using System.Net;
using EF.VenueBooking.Application.Queries;

namespace EF.VenueBooking.Api.Controllers
{
    [Route("api/[controller]")]
    public class VenuesController : Controller
    {
        private readonly VenueRepository _repo;
        private readonly VenueQueries _queries;
        private readonly IMediator _mediator;

        public VenuesController(VenueRepository repo, VenueQueries queries, IMediator mediator)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET: api/venues
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}", Name = "GetVenue")]
        public IActionResult GetById(Guid id)
        {
            var item = _queries.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/venues
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateVenueRendition rendition)
        {
            var id = Guid.NewGuid();
            var command = new CreateVenue(
                id,
                rendition.City,
                rendition.Address,
                rendition.Seats,
                rendition.DiscountCoupons.Select(_ => Tuple.Create(_.CouponCode, _.ProductName))
            );

            await _mediator.Send(command);

            var result = await _queries.Find(id);

            return CreatedAtRoute("GetVenue", new { id = result.VenueId }, result);
        }
    }
}
