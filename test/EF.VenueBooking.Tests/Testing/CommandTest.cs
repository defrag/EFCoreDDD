using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EF.VenueBooking.Tests.Testing
{
    public abstract class CommandTest
    {
        private IServiceProvider _services;

        public CommandTest()
        {
            var services = new ServiceCollection();
            var configBuilder = new ConfigurationBuilder();
            var config = configBuilder.Build();

            var startup = new TestStartup();
            startup.ConfigureServices(services, config);

            _services = services.BuildServiceProvider();
        }

        protected async Task Dispatch(IRequest command)
        {
            var mediatr = GetService<IMediator>();
            await mediatr.Send(command);
        }

        protected T GetService<T>()
            => _services.GetService<T>();
    }
}
