﻿using EF.VenueBooking.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace EF.VenueBooking.Api.Tests.Testing
{
    public class ApiFixture : IDisposable
    {
        public TestServer Server { get; }
        public HttpClient Client { get; }

        private VenueBookingContext _context;

        public ApiFixture()
        {
            Assembly startupAssembly = typeof(TestStartup).GetTypeInfo().Assembly;
            var contentRoot = Environment.CurrentDirectory;

            var builder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .UseEnvironment("Test")
                
                .UseStartup(typeof(TestStartup));

            Server = new TestServer(builder);
            Client = Server.CreateClient();
            _context = GetService<VenueBookingContext>();
            _context.Database.EnsureCreated();
        }

        public VenueBookingContext Context => _context;

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }

        public T GetService<T>()
            => Server.Host.Services.GetRequiredService<T>();
    }
}