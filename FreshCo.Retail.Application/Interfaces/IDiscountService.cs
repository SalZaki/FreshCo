namespace FreshCo.Retail.Application.Interfaces
{
    using Domain.Entities;

    public interface IDiscountService
    {
        Discount GetDiscountBySku(string Sku);
    }
}