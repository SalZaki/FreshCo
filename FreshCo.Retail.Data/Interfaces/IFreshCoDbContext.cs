namespace FreshCo.Retail.Data.Interfaces
{
    using System.Data.Entity;

    using FreshCo.Retail.Domain.Entities;

    public interface IFreshCoDbContext
    {
        DbSet<Product> Products { get; set; }

        DbSet<Discount> Discounts { get; set; }
    }
}