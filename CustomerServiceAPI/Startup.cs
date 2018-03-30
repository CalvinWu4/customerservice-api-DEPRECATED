﻿using System;
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
            services.AddDbContext<Context>(o => o.UseMySql(DB_URI));
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
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
                cfg.CreateMap<Entities.Ticket, Models.TicketDto>();
                cfg.CreateMap<Models.TicketForCreationDto, Entities.Ticket>();

                cfg.CreateMap<Entities.Client, Models.ClientDto>();
                cfg.CreateMap<Models.ClientDtoForCreation, Entities.Client>();
            });

            app.UseMvc();
        }
    }
}
