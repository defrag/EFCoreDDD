﻿using EF.VenueBooking.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EF.VenueBooking.Tests.Testing
{
    public sealed class CommandTest : IDisposable
    {
        private IServiceProvider _services;
        private TestStartup _startup;
        private VenueBookingContext _context;

        public CommandTest()
        {
            var services = new ServiceCollection();
            var configBuilder = new ConfigurationBuilder();
            var config = configBuilder.Build();

            _startup = new TestStartup();
            _startup.ConfigureServices(services, config);

            _services = services.BuildServiceProvider();

            _context = GetService<VenueBookingContext>();
            _context.Database.EnsureCreated();
        }

        public async Task Dispatch(IRequest command)
        {
            var mediatr = GetService<IMediator>();
            await mediatr.Send(command);
        }

        public T GetService<T>()
            => _services.GetService<T>();

        public VenueBookingContext Context => _context;

        public void Dispose()
        {
            _startup.Dispose();
        }
    }
}
