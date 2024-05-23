using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Ingredient
    {
        [Key] public int Id { get; set; }  

        public string Name { get; set; }

        public List<Pizzas> Pizzas { get; set; }

    }
}
