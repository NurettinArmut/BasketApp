namespace BasketApp.Core.Models
{
    public class Basket : BaseEntity
    {
        public int ProductId { get; set; }
        public int StockCount { get; set; }
        public decimal Amount { get; set; }
    }
}
