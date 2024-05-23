using la_mia_pizzeria_static.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models
{
    public class PizzasFormModel
    {
        public Pizzas Pizzas { get; set; }
        public List<Category>? Categories { get; set; }

        public List<SelectListItem>? Ingredients { get; set; }

        public List<string>? SelectedIngredients { get; set; }

        public void CreateIngredients()
        {
            this.Ingredients = new List<SelectListItem>();
            this.SelectedIngredients = new List<string>();
            var ingredientsFromDB = PizzaManager.GetAllIngredients();
            foreach (var ingredient in ingredientsFromDB)
            {
                bool isSelected = this.Pizzas.Ingredients?.Any(i => i.Id == ingredient.Id) == true;
                this.Ingredients.Add(new SelectListItem()
                {
                    Text = ingredient.Name,
                    Value = ingredient.Id.ToString(),
                    Selected = isSelected,
                });
                if (isSelected )
                {
                    this.SelectedIngredients.Add(ingredient.Id.ToString());
                }
            }
        }
    }
}
