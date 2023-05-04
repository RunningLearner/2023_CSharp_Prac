namespace Category;

static class Category
{
    static void Main(string[] args)
    {
        using var context = new CategoryDBContext();
        context.EnsureDatabaseCreatedAndMigrated();
        var category = new CategoryController(new CategoryRepositorySqlite(context), new CategoryPrinter());

        Console.WriteLine("[1] 기본값이 있는지 확인: ");
        var hasCategory = category.HasCategory();

        if (hasCategory)
        {
            Console.WriteLine("기본 데이터가 있습니다.");
        }
        else
        {
            Console.WriteLine("기본 데이터가 없습니다.");
        }

        Console.WriteLine("[2] 기본 데이터 출력: ");
        category.ReadCategories();

        Console.WriteLine("[3] 데이터 입력: ");
        category.AddCategory();

        Console.WriteLine("[4] 상세 보기: ");
        category.BrowseCategory();

        Console.WriteLine("[5] 데이터 수정: ");
        category.EditCategory();

        Console.WriteLine("[6] 데이터 삭제: ");
        category.DeleteCategory();

        Console.WriteLine("[7] 데이터 검색: ");
        category.SearchCategories();

        Console.WriteLine("[8] 페이징: ");
        category.PagingCategories();

        Console.WriteLine("[9] 정렬: ");
        category.OrderingCategories();
    }
}
