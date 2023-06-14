using Microsoft.EntityFrameworkCore;
using FirstApi.Models;

namespace FirstApi.Data
{
    public sealed class PizzaDbContext : DbContext
    {
        public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
            : base(options)
        {
            Pizzas = Set<Pizza>();
        }

        public DbSet<Pizza> Pizzas { get; set; }
    }
}
