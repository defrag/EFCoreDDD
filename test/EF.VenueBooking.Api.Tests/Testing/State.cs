using EF.VenueBooking.Application.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EF.VenueBooking.Api.Tests.Testing
{
    public sealed class State
    {
        private readonly IMediator _mediator;
        private readonly IServiceProvider _provider;

        public State(IServiceProvider provider)
        {
            _provider = provider;
            _mediator = (IMediator)_provider.GetService(typeof(IMediator));
        }

        internal async Task CreateVenue(
            Guid id,
            string city = "Cravov",
            string address = "Florianska 1",
            int seats = 10
        )
        {
            var coupons = new List<Tuple<string, string>>();
            await _mediator.Send(
                new CreateVenue(
                    id, city, address, seats, coupons
                )
            );
        }
    }
}
