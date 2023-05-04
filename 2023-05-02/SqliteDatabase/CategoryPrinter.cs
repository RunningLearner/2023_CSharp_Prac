namespace Category;

public class CategoryPrinter
{
    /// <summary>
    /// [0] 카테고리 출력 공통 메서드
    /// </summary>
    /// <param name="categories">카테고리 리스트</param>
    public void PrintCategories(List<CategoryModel> categories)
    {
        foreach (var category in categories)
        {
            Console.WriteLine($"{category.CategoryId} - {category.CategoryName}");
        }
        Console.WriteLine();
    }
}