using Microsoft.EntityFrameworkCore;
namespace Category;

public class CategoryDBContext : DbContext
{
    public DbSet<CategoryModel> Categories { get; set; }
    private const string DB_PATH = "category.db"; 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DB_PATH};");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryModel>(entity =>
        {
            entity.HasKey(e => e.CategoryId);
            entity.Property(e => e.CategoryName).IsRequired();
        });

        modelBuilder.Entity<CategoryModel>().HasData(
            new CategoryModel { CategoryId = 1, CategoryName = "책" },
            new CategoryModel { CategoryId = 2, CategoryName = "강의" },
            new CategoryModel { CategoryId = 3, CategoryName = "컴퓨터" }
        );
    }

    public void EnsureDatabaseCreatedAndMigrated()
    {
        if (Database.EnsureCreated())
        {
            Console.WriteLine("Database has been created!");
        }
        else
        {
            Console.WriteLine("Database already exists. Applying migrations...");
            Database.Migrate();
            Console.WriteLine("Migrations applied successfully!");
        }
    }
}
