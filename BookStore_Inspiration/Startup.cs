﻿using BookStore.Data;
using BookStore.Model;
using BookStore_Inspiration.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore_Inspiration
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
    
            services.AddIdentity<BookStoreUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<BookStoreDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<BookStoreDbContext>())
                {
                    context.Database.EnsureCreated();
                    //if (!context.Roles.Any())
                    //{
                    //    context.Roles.Add(new IdentityRole()
                    //    {
                    //        Name = "Admin",
                    //        NormalizedName = "ADMIN"
                    //    });
                    //    context.Roles.Add(new IdentityRole()
                    //    {
                    //        Name = "User",
                    //        NormalizedName = "USER"
                    //    });
                    //}

                    //context.SaveChanges();
                }
            }

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();



            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
