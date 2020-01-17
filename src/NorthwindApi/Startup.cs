using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MediatR;
using NorthwindApi.Model.Context;
using NorthwindApi.Model.Repository;
using AutoMapper;
using FluentValidation.AspNetCore;
using FluentValidation;
using NorthwindApi.Model.Entity;
using NorthwindApi.Controllers;

namespace NorthwindApi
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation();

            // Validators
            // https://fluentvalidation.net/aspnet
            //services.AddTransient<IValidator<Category>, CategoryValidator>();
            services.AddTransient<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>();
            services.AddTransient<IValidator<UpdateCategoryCommand>, UpdateCategoryCommandValidator>();

            services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(typeof(Startup));            

            services.AddScoped<IDbContext, DbContext>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            

            // override Model State
            /*
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)).ToList();

                    var result = new
                    {
                        Code = "00009",
                        Message = "Validation Errors",
                        Errors = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);

            app.UseMvc();
        }
    }
}
