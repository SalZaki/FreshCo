namespace FreshCo.Retail.Domain.Entities
{
    using System;

    public class Discount : BaseEntity
    {
        public string Sku { get; set; }

        public int Quantity { get; set; }

        public decimal Value { get; set; }
    }
}