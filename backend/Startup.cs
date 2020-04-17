using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using backend.Controllers.ActionFilter;
using backend.Controllers.Common;
using backend.Exceptions.Common;
using DataStore;
using DataStore.Context;
using DataStore.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace backend
{
    public class Startup
    {
        private const string DevelopmentCrossOriginPolicy = "LibraryOriginPolicyDev";
        private const string ProductionCrossOriginPolicy = "LibraryOriginPolicyProd";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(DevelopmentCrossOriginPolicy,
                builder =>
                {
                    builder
                        .WithOrigins("http://localhost:9999")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
                options.AddPolicy(ProductionCrossOriginPolicy,
                 builder =>
                 {
                     builder
                         .WithOrigins("http://abc.com")
                         .AllowAnyHeader()
                         .AllowAnyMethod();
                 });
            });


            services.AddControllers(options =>
                options.Filters.Add(new HttpResponseExceptionFilter()))
                .ConfigureApiBehaviorOptions(options =>
                {
                    // Convert ModelState annotations to json
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        return new BadRequestObjectResult(JsonUtil.ModelStateToJson(context.ModelState));
                    };
                });
            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("UserContext")));

            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await Task.CompletedTask;
                        // write production error page html
                    });
                });
                app.UseHsts();
            }

            app.UseRouting();

            // app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireCors(env.IsDevelopment() ? DevelopmentCrossOriginPolicy : ProductionCrossOriginPolicy);
            });
        }
    }
}
