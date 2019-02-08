namespace FreshCo.Retail.FeatureFiles.Steps
{
    using Moq;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    using System.Data.Entity;
    using System.Linq;

    using Application.Services;
    using Data.Interfaces;
    using Domain.Entities;

    [Binding]
    public class BaseCheckoutSteps : Steps
    {
        private readonly CheckoutContext _checkoutContext;

        public BaseCheckoutSteps(CheckoutContext checkoutContext)
        {
            _checkoutContext = checkoutContext;
        }

        [Given(@"I configure database context with following products")]
        public void GivenIConfigureDatabaseContextWithFollowingProducts(Table table)
        {
            var products = table.CreateSet<Product>().AsQueryable();

            var mockDbSet = new Mock<DbSet<Product>>();
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.Provider);
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.Expression);
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.ElementType);
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => products.GetEnumerator());

            ScenarioContext.Current.Set(mockDbSet, "mockProducts");
        }

        [Given(@"I configure database context with following discounts ""(.*)""")]
        public void GivenIConfigureDatabaseContextWithFollowingDiscounts(string discountScheme, Table table)
        {
            var discounts = table.CreateSet<Discount>().AsQueryable();

            var mockDbSet = new Mock<DbSet<Discount>>();
            mockDbSet.As<IQueryable<Discount>>().Setup(m => m.Provider).Returns(discounts.Provider);
            mockDbSet.As<IQueryable<Discount>>().Setup(m => m.Expression).Returns(discounts.Expression);
            mockDbSet.As<IQueryable<Discount>>().Setup(m => m.ElementType).Returns(discounts.ElementType);
            mockDbSet.As<IQueryable<Discount>>().Setup(m => m.GetEnumerator()).Returns(() => discounts.GetEnumerator());

            ScenarioContext.Current.Set(mockDbSet, "mockDiscounts");
        }

        [Given(@"I configure checkout service")]
        public void GivenThenIConfigureCheckOutService()
        {
            var mockProducts = ScenarioContext.Current.Get<Mock<DbSet<Product>>>("mockProducts");
            var mockDiscounts = ScenarioContext.Current.Get<Mock<DbSet<Discount>>>("mockDiscounts");
            var mockContext = new Mock<IFreshCoDbContext>();
            mockContext.Setup(p => p.Products).Returns(mockProducts.Object);
            mockContext.Setup(p => p.Discounts).Returns(mockDiscounts.Object);
            var productService = new ProductService(mockContext.Object);
            var discountService = new DiscountService(mockContext.Object);
            _checkoutContext.CheckoutService = new CheckoutService(productService, discountService);
        }
    }
}