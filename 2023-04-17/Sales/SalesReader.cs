namespace SalesCalculator
{
    public static class SalesReader
    {
        public static IEnumerable<Sale> ReadSales(string filePath)
        {
            var sales = new List<Sale>();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var items = line.Split(',');
                var sale = new Sale(shopName: items[0], productCategory: items[1], amount: int.Parse(items[2]));
                sales.Add(sale);
            }

            return sales;
        }
    }
}