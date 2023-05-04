namespace BooksLibrary;

public class Author
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime Birthday { get; set; }

    public string Gender { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}