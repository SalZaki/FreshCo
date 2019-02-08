namespace FreshCo.Retail.Data
{
    using Interfaces;
    using Domain.Entities;
    using System.Data.Entity;

    public class FreshCoDbContext : IFreshCoDbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Discount> Discounts { get; set; }

    }
}