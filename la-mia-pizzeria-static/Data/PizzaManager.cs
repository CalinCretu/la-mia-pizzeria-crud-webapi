using la_mia_pizzeria_static.Migrations;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Data

{
    public class PizzaManager
    {
        public static int CountDbPizzas()
        {
            using PizzasContext db = new PizzasContext();
            return db.Pizzas.Count();
        }

        public static List<Pizzas> GetAllPizzas()
        {
            using PizzasContext db = new PizzasContext();
            return db.Pizzas.ToList();
        }

        public static List<Category> GetAllCategories()
        {
            using PizzasContext db = new PizzasContext();
            return db.Categories.ToList();
        }

        public static List<Ingredient> GetAllIngredients()
        {
            using PizzasContext db = new PizzasContext();
            return db.Ingredients.ToList();
        }

        public static void ResetTable()
        {
            using (var db = new PizzasContext())
            {
                db.Pizzas.RemoveRange(db.Pizzas);
                db.SaveChanges();
            }
        }
        public static Pizzas VediPizza(int id, bool includeReferences = true)
        {
            using PizzasContext db = new PizzasContext();
            if (includeReferences)
                return db.Pizzas.Where(p => p.Id == id).Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault();
            return db.Pizzas.FirstOrDefault(p => p.Id == id);
        }

        public static Pizzas GetPizzaByName( string name )
        {
            using PizzasContext db = new PizzasContext();
            return db.Pizzas.FirstOrDefault( p => p.Name == name);
        }

        public static Ingredient GetIngredientById(int id)
        {
            using PizzasContext db = new PizzasContext();
            return db.Ingredients.FirstOrDefault(i => i.Id == id);
        }

        public static void InsertPizza(Pizzas pizza, List<string> SelectedIngredients = null)
        {
            using PizzasContext db = new PizzasContext();
            if (SelectedIngredients != null)
            {
                pizza.Ingredients = new List<Ingredient>();
                foreach (var ingredientId in SelectedIngredients)
                {
                    int id = int.Parse(ingredientId);
                    var ingredient = db.Ingredients.FirstOrDefault(t => t.Id == id);
                    pizza.Ingredients.Add(ingredient);
                }
            }
            db.Pizzas.Add(pizza);
            db.SaveChanges();
        }

        public static bool UpdatePizza(int id, string name, string description, double price, int? categoryId, List<string> ingredients)
        {
            using PizzasContext db = new PizzasContext();
            var pizza = db.Pizzas.Where(x => x.Id == id).Include(x => x.Ingredients).FirstOrDefault();

            if (pizza == null)
                return false;

            pizza.Name = name;
            pizza.Description = description;
            pizza.Price = price;
            pizza.CategoryId = categoryId;

            pizza.Ingredients.Clear();
            if( ingredients != null )
            {
                foreach( var ingredient in ingredients)
                {
                    int ingredientId = int.Parse(ingredient);
                    var ingredientFromDb = db.Ingredients.FirstOrDefault(x => x.Id == ingredientId);
                    pizza.Ingredients.Add(ingredientFromDb);
                }
            }

            db.SaveChanges();

            return true;
        }
        public static bool DeletePizza(int id)
        {
            using PizzasContext db = new PizzasContext();
            var post = db.Pizzas.FirstOrDefault(p => p.Id == id);

            if (post == null)
                return false;

            db.Pizzas.Remove(post);
            db.SaveChanges();

            return true;
        }

        public static void Seed()
        {
            if (PizzaManager.CountDbPizzas() == 0)
            {
                PizzaManager.InsertPizza(new Pizzas("Quattro Stagioni", "Pomodoro, fiordilatte, funghi, prosciutto cotto, carciofi e olive nere", "~/img/quattro-stagioni.jpeg", 7));
                PizzaManager.InsertPizza(new Pizzas("Capricciosa", "Pomodoro, fiordilatte, funghi, prosciutto cotto, carciofi e olive", "~/img/capricciosa.jpg", 7));
                PizzaManager.InsertPizza(new Pizzas("Margherita D.O.P.", "Pomodoro San Marzano D.O.P., mozzarella di bufala campana D.O.P. e basilico fresco", "~/img/margherita.jpg", 8));
                PizzaManager.InsertPizza(new Pizzas("Ortolana", "Pomodoro, fiordilatte, melanzane, zucchine, peperoni e cipolla", "~/img/ortolana.jpeg", 6.5));
                PizzaManager.InsertPizza(new Pizzas("Tonno e Cipolla", "Pomodoro, fiordilatte, tonno e cipolla", "~/img/tonno-cipolla.jpeg", 6));
                PizzaManager.InsertPizza(new Pizzas("Quattro Formaggi", "Pomodoro, fiordilatte, gorgonzola, provola affumicata, pecorino romano e parmigiano reggiano", "~/img/quattro-formaggi.jpeg", 7.5));
                PizzaManager.InsertPizza(new Pizzas("Calzone", "Pomodoro, fiordilatte, prosciutto cotto e funghi, chiusa a calzone", "~/img/calzone.jpeg", 7));
                PizzaManager.InsertPizza(new Pizzas("Rustica", "Pomodoro, fiordilatte, salsiccia, patate e rosmarino", "~/img/rustica.jpeg", 6.5));
                PizzaManager.InsertPizza(new Pizzas("Tartufata", "Pomodoro, fiordilatte, funghi champignon, prosciutto crudo e crema di tartufo", "~/img/tartufata.jpeg", 8));
                PizzaManager.InsertPizza(new Pizzas("Salsiccia e Friarielli", "Pomodoro, fiordilatte, salsiccia e friarielli", "~/img/salsiccia-friarielli.jpeg", 7));
            }
        }
    }
}
