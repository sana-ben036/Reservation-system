using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem
{
    public class Startup
    {

       private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseMySql(configuration.GetConnectionString("MySQLConnection")));

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;  // for accept ouwn default routing
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); // any url to check it will need authentication as condition , if not any one will be able to check local url (any action without authorize)
                options.Filters.Add(new AuthorizeFilter(policy));

            }).AddXmlSerializerFormatters();


            //should install package : microsoft.aspnetcore.authentication.google
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "821422014065-qbrbbgbqaensm4fpddvv55mbj21gvmbc.apps.googleusercontent.com";
                    options.ClientSecret = "Tn8Qj35FIpiqVX5IoMwePbcm";

                });


           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Account}/{action=login}/{id ?}");
            });



            app.UseRouting();


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
