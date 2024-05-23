using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaWebApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllPizzas()
        {
            var pizzas = PizzaManager.GetAllPizzas();
            return Ok(pizzas);
        }

        [HttpGet]
        public IActionResult GetPizzaById(int id)
        {
            var pizza = PizzaManager.VediPizza(id);
            if (pizza == null)
                return NotFound();
            return Ok(pizza);
        }

        [HttpGet("{name}")]
        public IActionResult GetPizzaByName(string name)
        {
            var pizza = PizzaManager.GetPizzaByName(name);
            if (pizza == null)
                return NotFound();
            return Ok(pizza);
        }

        [HttpPost]
        public IActionResult CreatePizza([FromBody] Pizzas pizzas)
        {
            PizzaManager.InsertPizza(pizzas, null);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdatePizza(int id, [FromBody] Pizzas pizzas)
        {
            var oldPizza = PizzaManager.VediPizza(id);
            if (oldPizza == null)
                return NotFound();

            var success = PizzaManager.UpdatePizza(
                id,
                pizzas.Name,
                pizzas.Description,
                pizzas.Price,
                pizzas.CategoryId,
                null // Passiamo null per gli ingredienti per ora
            );

            if (!success)
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore durante l'aggiornamento della pizza.");

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePizza(int id)
        {
            bool success = PizzaManager.DeletePizza(id);
            if (!success)
                return NotFound();
            return Ok();
        }
    }
}
