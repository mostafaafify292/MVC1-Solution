using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MVC__BLL_.Interfacies;
using MVC__BLL_.Repositories;
using MVC1.Helper;
using MVC1__DAL_.Data;
using MVC1__DAL_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Builder = WebApplication.CreateBuilder(args);
            #region Configure Services
            Builder.Services.AddControllersWithViews();
            Builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(Builder.Configuration.GetConnectionString("DefultConnection")));
            Builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));
            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddScoped<RoleManager<IdentityRole>>();
            Builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                config =>
                {
                    //config.Password.RequiredUniqueChars = 2;
                    //config.Password.RequireDigit = true;
                    //config.Password.RequireLowercase = true;
                    //config.Password.RequireUppercase = true;
                    config.Password.RequireNonAlphanumeric = false;
                    //config.User.RequireUniqueEmail = true;
                    //config.Lockout.MaxFailedAccessAttempts = 3;
                    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                }
                ).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            #endregion
            var app = Builder.Build();
            #region Cofigure

            if (Builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            #endregion
            app.Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
