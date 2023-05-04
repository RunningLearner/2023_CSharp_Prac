namespace Category;

public class CategoryController
{
    // 리포지토리 클래스 참조 
    private readonly ICategoryRepository<CategoryModel> _categoryRepository;
    private readonly CategoryPrinter _categoryPrinter;

    public CategoryController(ICategoryRepository<CategoryModel> categoryRepository, CategoryPrinter categoryPrinter)
    {
        _categoryRepository = categoryRepository;
        _categoryPrinter = categoryPrinter;
    }

    /// <summary>
    /// [1] 건수
    /// </summary>
    public bool HasCategory()
    {
        return _categoryRepository.CountCategory() > 0;
    }

    /// <summary>
    /// [2] 출력
    /// </summary>
    public void ReadCategories()
    {
        var categories = _categoryRepository.Read();
        _categoryPrinter.PrintCategories(categories);
    }

    /// <summary>
    /// [3] 입력
    /// </summary>
    public void AddCategory()
    {
        var category = new CategoryModel() { CategoryName = "생활용품" };
        _categoryRepository.Add(category);
        ReadCategories();
    }

    /// <summary>
    /// [4] 상세
    /// </summary>
    public void BrowseCategory()
    {
        int categoryId = 4;
        var category = _categoryRepository.Browse(categoryId);

        if (category != null)
        {
            Console.WriteLine($"{category.CategoryId} - {category.CategoryName}");
        }
        else
        {
            Console.WriteLine($"{categoryId}번 카테고리가 없습니다.");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// [5] 수정
    /// </summary>
    public void EditCategory()
    {
        _categoryRepository.Edit(new CategoryModel { CategoryId = 4, CategoryName = "가전용품" });
        ReadCategories();
    }

    /// <summary>
    /// [6] 삭제
    /// </summary>
    public void DeleteCategory()
    {
        int categoryId = 1;
        _categoryRepository.Delete(categoryId);
        Console.WriteLine($"{categoryId}번 데이터를 삭제합니다.");
        ReadCategories();
    }

    /// <summary>
    /// [7] 검색
    /// </summary>
    public void SearchCategories()
    {
        var query = "강의";
        var categories = _categoryRepository.Search(query);
        _categoryPrinter.PrintCategories(categories);
    }

    /// <summary>
    /// [8] 페이징
    /// </summary>
    public void PagingCategories()
    {
        var categories = _categoryRepository.Paging(2, 2);

        if (categories.Count > 1)
        {
            categories.RemoveAt(0);
        }

        _categoryPrinter.PrintCategories(categories);
    }

    /// <summary>
    /// [9] 정렬
    /// </summary>
    public void OrderingCategories()
    {
        var categories = _categoryRepository.Ordering(OrderOption.Descending);
        _categoryPrinter.PrintCategories(categories.ToList());
    }
}