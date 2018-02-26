using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF.VenueBooking.Domain;
using EF.VenueBooking.Api.Renditions;
using EF.VenueBooking.Application.Commands;
using MediatR;
using EF.VenueBooking.Application.Queries;
using LanguageExt;
using EF.VenueBooking.Application.ViewModels;
using static EF.VenueBooking.Api.Http.ResponseGenerator;

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

        [HttpGet("{id}", Name = "GetVenue")]
        public Task<IActionResult> GetById(Guid id)
            => from result in QueryVenue(id)
               select result.Match<IActionResult>(
                    Some: Ok,
                    None: NotFound
                );

        // POST api/venues
        [HttpPost]
        public Task<IActionResult> Post([FromBody]CreateVenueRendition rendition)
            => from cmd in CreateVenueCommand(rendition).AsTask()
               from _ in CreateVenue(cmd)
               from result in QueryVenue(cmd.VenueId)
               select result.Match<IActionResult>(
                   Some: item => CreatedAtRoute("GetVenue", new { id = item.VenueId }, item),
                   None: NotFound
               );

        // POST api/venues/{id}/register
        [HttpPost("{id}/register")]
        public Task<IActionResult> Register(Guid id, [FromBody]RegisterAttendeeToVenueRendition rendition)
          => from cmd in CreateRegisterCommand(rendition, id).AsTask()
             from result in RegisterToVenue(cmd)
             select result.Match(
                   Right: (_) => NoContent(),
                   Left: HandleError
             );

        private Task<Option<VenueViewModel>> QueryVenue(Guid venueId) 
            => _queries.Find(venueId);

        private CreateVenue CreateVenueCommand(CreateVenueRendition rendition) =>
            new CreateVenue(
                Guid.NewGuid(),
                rendition.City,
                rendition.Address,
                rendition.Seats,
                rendition.DiscountCoupons.Select(_ => (_.CouponCode, _.ProductName))
            );

        private RegisterAttendeeToVenue CreateRegisterCommand(RegisterAttendeeToVenueRendition rendition, Guid venueId)
            => new RegisterAttendeeToVenue(venueId, rendition.AttendeeId);

        private Task<Either<VenueError, LanguageExt.Unit>> RegisterToVenue(RegisterAttendeeToVenue command)
            => _mediator.Send(command);

        private async Task<LanguageExt.Unit> CreateVenue(CreateVenue command)
        {
            await _mediator.Send(command);

            return new LanguageExt.Unit();
        }

        private IActionResult HandleError(VenueError e)
        {
            switch (e)
            {
                case VenueNotFound err:
                    return ResourceNotFound(err.Value);
                default:
                    return ErrorOccured(e.Value);
            }
        }
    }
}
