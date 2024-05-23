using la_mia_pizzeria_static.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace la_mia_pizzeria_static
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                       // var connectionString = builder.Configuration.GetConnectionString("PizzasContextConnection") ?? throw new InvalidOperationException("Connection string 'PizzasContextConnection' not found.");

            // Add services to the container.
            builder.Services.AddDbContext<PizzasContext>();

            builder.Services
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PizzasContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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
                name: "default",
                pattern: "{controller=Pizza}/{action=Index}/{id?}");

            app.MapRazorPages();

            PizzaManager.Seed();
            app.Run();
        }
    }
}