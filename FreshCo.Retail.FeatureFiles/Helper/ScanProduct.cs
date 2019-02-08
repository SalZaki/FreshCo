namespace FreshCo.Retail.FeatureFiles.Helper
{
    using System;

    public class ScanProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
