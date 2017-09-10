using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EF.VenueBooking.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using EF.VenueBooking.Domain;

namespace EF.VenueBooking.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            ConfigureDbContext(services);
            ConfigureVenueServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<VenueBookingContext>(GetDbOptions());
        }

        private Action<DbContextOptionsBuilder> GetDbOptions()
        {
            //return options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("EF.VenueBooking.Api"));
            return options => options.UseInMemoryDatabase($"InMemVenuesDb");
        }

        private void ConfigureVenueServices(IServiceCollection services)
        {
            services.AddTransient<VenueRepository, EntityFrameworkVenueRepository>();
        }
    }
}
