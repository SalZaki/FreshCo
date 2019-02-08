namespace FreshCo.Retail.Application.Services
{
    using System.Linq;
    using Domain.Entities;
    using Data.Interfaces;
    using Exceptions;
    using Interfaces;

    public sealed class ProductService : IProductService
    {
        private readonly IFreshCoDbContext _freshCoDbContext;

        public ProductService(IFreshCoDbContext freshCoDbContext)
        {
            _freshCoDbContext = freshCoDbContext;
        }

        public Product GetProductBySku(string sku)
        {
            var product = _freshCoDbContext.Products.Where(x => x.Sku == sku).Single();
            if (product == null)
            {
                throw new NotFoundException(nameof(Product), sku);
            }
            return product;
        }
    }
}