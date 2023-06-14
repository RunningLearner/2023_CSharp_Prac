using FirstApi.Models;
using FirstApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Services;

public sealed class PizzaService
{
    private readonly PizzaDbContext _context;

    public PizzaService(PizzaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pizza>> GetAllAsync() => await _context.Pizzas.ToListAsync();

    public async Task<Pizza?> GetAsync(int id)
    {
        return await _context.Pizzas.FindAsync(id);
    }

    public async Task AddAsync(Pizza pizza)
    {
        _context.Pizzas.Add(pizza);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var pizza = await _context.Pizzas.FindAsync(id);
        if (pizza != null)
        {
            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(Pizza pizza)
    {
        var existingPizza = await _context.Pizzas.FindAsync(pizza.Id)
            ?? throw new ArgumentException("Pizza with given id not found", nameof(pizza));
        existingPizza.Name = pizza.Name;
        existingPizza.IsGlutenFree = pizza.IsGlutenFree;

        await _context.SaveChangesAsync();
    }
}
