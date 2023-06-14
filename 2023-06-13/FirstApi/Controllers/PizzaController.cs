using FirstApi.Models;
using FirstApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class PizzaController : ControllerBase
{
    private readonly PizzaService _pizzaService;

    public PizzaController(PizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    // GET all action
    [HttpGet]
    public async Task<List<Pizza>> GetAllAsync() =>
        await _pizzaService.GetAllAsync();

    // GET by Id action
    [ActionName("GetAsync")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Pizza>> GetAsync(int id)
    {
        var pizza = await _pizzaService.GetAsync(id);

        if (pizza == null)
        {
            return NotFound();
        }

        return pizza;
    }

    // POST action
    [HttpPost]
    public async Task<IActionResult> CreateAsync(Pizza pizza)
    {
        await _pizzaService.AddAsync(pizza);
        return CreatedAtAction(nameof(GetAsync), new { id = pizza.Id }, pizza);
    }

    // PUT action
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, Pizza pizza)
    {
        if (id != pizza.Id)
        {
            return BadRequest();
        }

        await _pizzaService.UpdateAsync(pizza);

        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var pizza = await _pizzaService.GetAsync(id);

        if (pizza is null)
        {
            return NotFound();
        }

        await _pizzaService.DeleteAsync(id);

        return NoContent();
    }
}