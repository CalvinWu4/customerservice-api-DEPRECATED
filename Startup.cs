using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerServiceAPI.Entities;
using CustomerServiceAPI.Models;
using CustomerServiceAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CustomerServiceAPI
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
            string DB_URI = Configuration["DB"];

            services.AddMvc();
            services.AddDbContext<TicketContext>(o => o.UseMySql(DB_URI));
            services.AddScoped<ITicketRepository, TicketRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Ticket, Models.TicketDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    Line1 = src.AddressLine1,
                    Line2 = src.AddressLine2,
                    City = src.AddressCity,
                    State = src.AddressState,
                    Zipcode = src.AddressZipcode,
                    Country = src.AddressCountry,
                }));

                cfg.CreateMap<Models.TicketForCreationDto, Entities.Ticket>();
            });

            app.UseMvc();
        }
    }
}
