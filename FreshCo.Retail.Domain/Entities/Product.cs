namespace FreshCo.Retail.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Sku { get; set; }

        public decimal Price { get; set; }
    }
}