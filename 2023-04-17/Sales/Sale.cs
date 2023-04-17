namespace SalesCalculator
{
    public class Sale
    {
        public string ShopName { get; }
        public string ProductCategory { get; }
        public int Amount { get; }

        public Sale(string shopName, string productCategory, int amount)
        {
            ShopName = shopName;
            ProductCategory = productCategory;
            Amount = amount;
        }
    }
}