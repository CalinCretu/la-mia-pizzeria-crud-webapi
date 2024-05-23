using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Data
{
    public class PizzasContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Pizzas> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=InitialPizzasDB;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}