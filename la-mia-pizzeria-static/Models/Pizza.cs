using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace la_mia_pizzeria_static.Models
{
    [Table("pizze")]
    [Index(nameof(Id), IsUnique = true)]
    public class Pizzas
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descrizione è obbligatoria")]
        [MinLength(5, ErrorMessage = "La descrizione deve contenere almeno 5 caratteri")]
        public string Description { get; set; }
        [Url(ErrorMessage = "Il campo immagine deve essere un URL valido")]
        public string? Image { get; set; }
        [Required(ErrorMessage = "Il prezzo è obbligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Il prezzo deve essere maggiore di zero")]
        public double Price { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Ingredient>? Ingredients { get; set; }

        public Pizzas(string name, string description, string immage, double price)
        {
            Name = name;
            Description = description;
            Image = immage;
            Price = price;
        }
        public Pizzas(int id, string name, string description, string image, double price)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
            Price = price;
        }

        public Pizzas() { }

        public string GetDisplayedCategory()
        {
            if (Category == null)
                return "Nessuna categoria";
            return Category.Title;

            // sintetizzata
            //return Category?.Title ?? "Nessuna categoria";
        }
    }
}