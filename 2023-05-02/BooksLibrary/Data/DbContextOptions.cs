using Microsoft.EntityFrameworkCore;
namespace BooksLibrary;

public static class DbContextConfigurations
{
    public static void ConfigureSqliteOptions(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = "books.db";
        optionsBuilder.UseSqlite($"Data Source={dbPath};");
    }

    public static void ConfigureMariaDbOptions(DbContextOptionsBuilder optionsBuilder, string connectionString)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}