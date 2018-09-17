using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartHR.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using SmartHR.Service.Services;
using SmartHR.Domain.Interfaces;
using SmartHR.Domain.Interfaces.Services;
using SmartHR.Domain.Interfaces.Repositories;
using SmartHR.Infra.Data.Repository;
using SmartHR.Domain.Entities;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace SmartHR.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SmartHRContext>(opt =>
                opt.UseInMemoryDatabase("SmartHRContext"));

            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();

            services.AddScoped<IVagaService, VagaService>();
            services.AddScoped<IVagaRepository, VagaRepository>();

            services.AddScoped<ICandidaturaService, CandidaturaService>();
            services.AddScoped<ICandidaturaRepository, CandidaturaRepository>();

            //Prevenção de erro de self referencing loop no mapeamento das entidades.
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}
