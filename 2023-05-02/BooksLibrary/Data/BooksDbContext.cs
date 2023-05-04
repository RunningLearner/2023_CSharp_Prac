using Microsoft.EntityFrameworkCore;
namespace BooksLibrary;

public class BooksDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public BooksDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Title).IsRequired();
            entity.Property(b => b.PublishedYear).IsRequired();
            entity.HasOne(b => b.Author).WithMany(a => a.Books);
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Name).IsRequired();
            entity.Property(a => a.Gender).IsRequired();
            entity.Property(a => a.Birthday).IsRequired();
        });

        Console.WriteLine("Tables Created Successfully!");
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