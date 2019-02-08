namespace FreshCo.Retail.Application.Interfaces
{
    using System.Collections.Generic;
    using Domain.Entities;

    public interface ICheckoutService
    {
        IReadOnlyList<Product> Products { get; }

        decimal Total { get; }

        void ScanProduct(string sku, int quantity);
    }
}