using LAB4_RPBDIS.Middleware;
using LAB4_RPBDIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB4_RPBDIS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.CacheProfiles.Add("Caching",
                    new CacheProfile()
                    {
                        Duration = 266
                    });
                options.CacheProfiles.Add("NoCaching",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                    });
            });
            builder.Services.AddSession();
            // Добавляем контекст данных
            builder.Services.AddDbContext<RailwayTrafficContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RailwayTrafficContext")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            // Добавляем поддержку сессий
            app.UseSession();

            // Добавляем компонент middleware по инициализации базы данных и производим инициализацию базы
            app.UseDbInitializer();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}