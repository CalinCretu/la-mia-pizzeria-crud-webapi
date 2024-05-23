using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Category
    {
        [Key] public int Id { get; set; }

        public string Title { get; set; }

        public List<Pizzas> Pizzas { get; set; }

        public Category() { }


    }
}
