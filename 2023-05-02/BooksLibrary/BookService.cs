using Microsoft.EntityFrameworkCore;

namespace BooksLibrary;

public class BookService
{
    private readonly BooksDbContext _dbContext;

    public BookService(BooksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void RunDemo()
    {
        InsertBooks();
        AddAuthors();
        AddBooks();
        DisplayAllBooks();
        DisplayLongestBook();
        DisplayThreeOldBook();
        DisplayAuthorAndBookDetails();
    }

    // The rest of the methods from the original BooksLibrary class go here, with the following changes:
    // 1. Remove the "" keyword from each method signature.
    // 2. Replace all instances of "_dbContext" with "_dbContext".

    private void UpdateBook()
    {
        var book = _dbContext.Books.Single(b => b.Title == "별의 계승자");
        book.PublishedYear = 2016;
        _dbContext.SaveChanges();
    }

    private void DeleteBook()
    {
        var book = _dbContext.Books.SingleOrDefault(b => b.Id == 10);

        if (book != null)
        {
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }

    private void AddBooks()
    {
        var author1 = _dbContext.Authors.FirstOrDefault(a => a.Name == "애거사 크리스티");

        var book1 = new Book
        {
            Title = "그리고 아무도 없었다",
            PublishedYear = 1939,
            Author = author1,
        };

        _dbContext.Books.Add(book1);
        var author2 = _dbContext.Authors.FirstOrDefault(a => a.Name == "찰스 디킨스");

        var book2 = new Book
        {
            Title = "두 도시 이야기",
            PublishedYear = 1859,
            Author = author2
        };

        _dbContext.Books.Add(book2);
        _dbContext.SaveChanges();
    }

    private void InsertBooks()
    {
        var book1 = new Book
        {
            Title = "별의 계승자",
            PublishedYear = 1977,

            Author = new Author
            {
                Birthday = new DateTime(1941, 6, 27),
                Gender = "M",
                Name = "제임스 P. 호건"
            }
        };

        _dbContext.Books.Add(book1);

        var book2 = new Book
        {
            Title = "타임머신",
            PublishedYear = 1895,

            Author = new Author
            {
                Birthday = new DateTime(1866, 9, 21),
                Gender = "M",
                Name = "허버트 조지 웰즈"
            }
        };

        _dbContext.Books.Add(book2);
        _dbContext.SaveChanges();
    }

    private void DisplayAllBooks()
    {
        var books = GetBooks();

        foreach (var book in books)
        {
            Console.WriteLine($"{book.Title} {book.PublishedYear}");
        }
    }

    private void DisplayLongestBook()
    {
        var books = GetBooks();
        var longestBook = books.OrderByDescending(b => b.Title.Length).First();
        Console.WriteLine($"가장 긴 책의 제목: {longestBook.Title}");
    }

    private void DisplayThreeOldBook()
    {
        var books = GetBooks();
        var oldestBooks = books.OrderBy(b => b.PublishedYear).Take(3);
        Console.WriteLine("가장 오래된 3권의 책:");

        foreach (var book in oldestBooks)
        {
            Console.WriteLine($"{book.Title} ({book.PublishedYear}) by {book.Author.Name}");
        }
    }

    private void DisplayAuthorAndBookDetails()
    {
        var books = GetBooks();
        var authors = books
            .GroupBy(b => b.Author.Name)
            .Select(g => new { AuthorName = g.Key, Books = g.ToList() });

        foreach (var author in authors)
        {
            Console.WriteLine($"Author: {author.AuthorName}");
            Console.WriteLine("Books: ");

            foreach (var book in author.Books)
            {
                Console.WriteLine($"  Title: {book.Title}");
                Console.WriteLine($"  Published Year: {book.PublishedYear}");
            }
        }
    }

    private IEnumerable<Book> GetBooks()
    {
        return _dbContext.Books.Include(b => b.Author).ToList();
    }

    private void AddAuthors()
    {
        var author1 = new Author
        {
            Birthday = new DateTime(1890, 09, 15),
            Gender = "F",
            Name = "애거사 크리스티"
        };

        _dbContext.Authors.Add(author1);

        var author2 = new Author
        {
            Birthday = new DateTime(1812, 02, 07),
            Gender = "M",
            Name = "찰스 디킨스"
        };

        _dbContext.Authors.Add(author2);
        _dbContext.SaveChanges();
    }
}
