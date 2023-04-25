namespace SalesCalculator
{
    public class Sale
    {
        public string ShopName;
        public string ProductCategory;
        public int Amount;

        public Sale(string shopName, string productCategory, int amount)
        {
            ShopName = shopName;
            ProductCategory = productCategory;
            Amount = amount;
        }
    }
}