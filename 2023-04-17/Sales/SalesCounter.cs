namespace SalesCalculator
{
    public class SalesCounter
    {
        private readonly IEnumerable<Sale> _sales;

        public SalesCounter(string filePath)
        {
            _sales = SalesReader.ReadSales(filePath);
        }

        public IDictionary<string, int> GetPerStoreSales()
        {
            var dict = new Dictionary<string, int>();

            foreach (var sale in _sales)
            {
                if (dict.ContainsKey(sale.ShopName))
                {
                    dict[sale.ShopName] += sale.Amount;
                }
                else
                {
                    dict[sale.ShopName] = sale.Amount;
                }
            }

            return dict;
        }

        public IDictionary<string, int> GetPerCategorySales()
        {
            var dict = new Dictionary<string, int>();

            foreach (var sale in _sales)
            {
                if (dict.ContainsKey(sale.ProductCategory))
                {
                    dict[sale.ProductCategory] += sale.Amount;
                }
                else
                {
                    dict[sale.ProductCategory] = sale.Amount;
                }
            }

            return dict;
        }
    }
}