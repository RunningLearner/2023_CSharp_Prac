using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BooksLibrary;

static class BooksLibrary
{
    static void Main(string[] args)
    {
        var serviceProvider = ConfigureServices();

        var dbContext = serviceProvider.GetService<BooksDbContext>();
        dbContext!.EnsureDatabaseCreatedAndMigrated();

        var bookService = serviceProvider.GetService<BookService>();
        bookService!.RunDemo();
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // SQLite 사용하기
        // services.AddDbContext<BooksDbContext>(options => DbContextConfigurations.ConfigureSqliteOptions(options));

        // MariaDB 사용하기
        const string connectionString = "Server=localhost;Database=bookslibrary;User=root;Password=1234;";
        services.AddDbContext<BooksDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddTransient<BookService>();

        return services.BuildServiceProvider();
    }
}
