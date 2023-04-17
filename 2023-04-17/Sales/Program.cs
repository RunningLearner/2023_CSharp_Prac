using SalesCalculator;

InitializeSalesCalculator();

static void InitializeSalesCalculator()
{
    var sales = new SalesCounter("sales.csv");
    var input = string.Empty;
    IDictionary<string, int> amountPerStore;

    while (input?.Length == 0)
    {
        Console.WriteLine("지점별 매출을 보시려면 1번을");
        Console.WriteLine("종류별 매출을 보시려면 아무키나");
        Console.WriteLine("눌러주세요.");
        input = Console.ReadLine()?.Trim() ?? string.Empty;
    }

    if (string.Equals(input, "1", StringComparison.Ordinal))
    {
        amountPerStore = sales.GetPerStoreSales();
    }
    else
    {
        amountPerStore = sales.GetPerCategorySales();
    }

    foreach (var obj in amountPerStore)
    {
        Console.WriteLine("{0} {1}", obj.Key, obj.Value);
    }
}
