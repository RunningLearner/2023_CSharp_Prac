using Microsoft.EntityFrameworkCore;

public class BloggingContext : DbContext
{
    private const string FileName = "blogging.db";
    public string DbPath { get;}

    public DbSet<Blog> Blogs { get; set; }

    public DbSet<Post> Posts { get; set; }

    public BloggingContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, FileName);
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}