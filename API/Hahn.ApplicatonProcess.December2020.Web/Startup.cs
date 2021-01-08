using AutoMapper;
using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Data.Repository.Implementation;
using Hahn.ApplicatonProcess.December2020.Data.Repository.Interface;
using Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Implementation;
using Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Interface;
using Hahn.ApplicatonProcess.December2020.Web.Validator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web
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

            services.AddControllers();
            services.AddDbContext<HahnDbContext>(options => options.UseInMemoryDatabase(databaseName: Configuration["DatabaseName"]));
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = c =>
                {
                    var errors = c.ModelState.Where(x => x.Value.Errors.Count > 0).Select(p => new { field = p.Key, error = p.Value.Errors.Select(t => t.ErrorMessage) });

                    return new BadRequestObjectResult(errors.ToList());
                };
            })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ApplicantValidator>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn.ApplicatonProcess.December2020.Web", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicatonProcess.December2020.Web v1"));
            }
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
