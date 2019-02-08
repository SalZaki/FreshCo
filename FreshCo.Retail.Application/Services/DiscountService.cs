namespace FreshCo.Retail.Application.Services
{
    using System.Linq;
    using Interfaces;
    using Data.Interfaces;
    using Domain.Entities;
    public sealed class DiscountService : IDiscountService
    {
        private readonly IFreshCoDbContext _freshCoDbContext;

        public DiscountService(IFreshCoDbContext freshCoDbContext)
        {
            _freshCoDbContext = freshCoDbContext;
        }

        public Discount GetDiscountBySku(string sku)
        {
            return _freshCoDbContext
                .Discounts
                .Where(x => x.Sku.Equals(sku)).SingleOrDefault();
        }
    }
}