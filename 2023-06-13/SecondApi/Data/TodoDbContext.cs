using Microsoft.EntityFrameworkCore;

namespace SecondApi.Data;

public sealed class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
        TodoItems = Set<TodoItem>();
    }

    public DbSet<TodoItem> TodoItems { get; set; }
}