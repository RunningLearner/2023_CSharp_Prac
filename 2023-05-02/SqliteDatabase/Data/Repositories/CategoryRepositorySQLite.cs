using Microsoft.Data.Sqlite;
namespace Category;

public class CategoryRepositorySqlite : ICategoryRepository<CategoryModel>
{
    private readonly CategoryDBContext _context;

    public CategoryRepositorySqlite(CategoryDBContext context)
    {
        _context = context;
    }

    public CategoryModel Add(CategoryModel model)
    {
        // 가장 큰 CategoryId에 1 더한 값으로 새로운 CategoryId 생성
        // 실제 데이터베이스에서는 자동 증가값(시퀀스)을 사용
        _context.Categories.Add(model);
        _context.SaveChanges();

        return model;
    }

    public CategoryModel Browse(int id)
    {
        return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
    }

    public bool Delete(int id)
    {
        var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

        if (category == null)
        {
            return false;
        }

        _context.Categories.Remove(category);
        _context.SaveChanges();
        return true;
    }

    public bool Edit(CategoryModel model)
    {
        var category = _context.Categories.FirstOrDefault(c => c.CategoryId == model.CategoryId);

        if (category is null) return false;

        category.CategoryName = model.CategoryName;
        _context.SaveChanges();
        return true;
    }

    public int CountCategory()
    {
        return _context.Categories.Count();
    }

    public IEnumerable<CategoryModel> Ordering(OrderOption orderOption)
    {
        return orderOption switch
        {
            OrderOption.Ascending => _context.Categories.OrderBy(c => c.CategoryName),
            OrderOption.Descending => _context.Categories.OrderByDescending(c => c.CategoryName),
            _ => _context.Categories,
        };
    }

    public List<CategoryModel> Paging(int pageNumber, int pageSize)
    {
        const int defaultPageNumber = 1;
        const int defaultPageSize = 10;

        if (ValidatePaging(pageNumber, pageSize) is not true)
        {
            pageNumber = defaultPageNumber;
            pageSize = defaultPageSize;
        }

        return _context.Categories
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    private static bool ValidatePaging(int pageNumber, int pageSize)
    {
        if (pageNumber < 1)
        {
            Console.WriteLine("pageNumber must be greater than or equal to 1.");
            return false;
        }

        if (pageSize < 1)
        {
            Console.WriteLine("pageSize must be greater than or equal to 1.");
            return false;
        }

        return true;
    }

    public List<CategoryModel> Read()
    {
        return _context.Categories.ToList();
    }

    public List<CategoryModel> Search(string query)
    {
        return _context.Categories
            .Where(category => category.CategoryName.Contains(query))
            .ToList();
    }
}
