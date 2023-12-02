using HubCount.Data;
using HubCount.Service;
using Microsoft.EntityFrameworkCore;

namespace HubCount
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<ExcelService>();


            // Use the application's configuration to get the connection string
            var connectionString = builder.Configuration.GetConnectionString("OnionSAConnection");
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html"); ;

            app.Run();

        }

    }
}