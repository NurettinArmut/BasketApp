namespace BasketApp.Core.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
