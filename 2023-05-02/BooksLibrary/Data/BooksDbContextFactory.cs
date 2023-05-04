using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace BooksLibrary;
public class BooksDbContextFactory : IDesignTimeDbContextFactory<BooksDbContext>
    {
        public BooksDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BooksDbContext>();
            DbContextConfigurations.ConfigureMariaDbOptions(optionsBuilder, "Server=localhost;Database=bookslibrary;User=root;Password=1234;");
            return new BooksDbContext(optionsBuilder.Options);
        }
    }