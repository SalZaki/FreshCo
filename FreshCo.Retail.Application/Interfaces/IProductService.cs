namespace FreshCo.Retail.Application.Interfaces
{
    using Domain.Entities;

    public interface IProductService
    {
        Product GetProductBySku(string sku);
    }
}