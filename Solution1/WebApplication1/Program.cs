using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ExamDay3DBContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
            }).AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.SignIn.RequireConfirmedEmail = false;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<ExamDay3DBContext>();
            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            //    {
            //        options.LoginPath = "~/Auth/Login";
            //        options.AccessDeniedPath = "~/Auth/AccessDenied";
            //        options.ReturnUrlParameter = "~/Auth/AccessDenied";

            //    });

            builder.Services.ConfigureApplicationCookie(options =>
            {

                options.LoginPath = "/Auth/Login";  //in your case /Account/Login
                options.LogoutPath = "/Auth/logout";
                options.AccessDeniedPath = "/Auth/AccessDenied";
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "Admin",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            PathConstants.RoothPath = builder.Environment.WebRootPath;
            app.Run();
        }
    }
}