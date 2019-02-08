namespace FreshCo.Retail.FeatureFiles.Steps
{
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;
    using Domain.Entities;
    using System.Linq;
    using FluentAssertions;
    using Helper;

    [Binding]
    public class CheckoutSteps : Steps
    {
        private readonly CheckoutContext _checkoutContext;

        public CheckoutSteps(CheckoutContext checkoutContext)
        {
            _checkoutContext = checkoutContext;
        }

        [When(@"I scan a single product with a valid sku ""(.*)""")]
        public void WhenIScanAProductWithAValidSku(string sku)
        {
            _checkoutContext.CheckoutService.ScanProduct(sku, 1);
        }

        [Then(@"I get the following product")]
        public void ThenIGetTheFollowingProduct(Table table)
        {
            // Arrange
            var product = table.CreateSet<Product>()
                .Single();

            // Act
            var sut = _checkoutContext.CheckoutService.Products
                .Single();

            // Assert
            sut.Should().NotBeNull().Should().Equals(product);
        }

        [When(@"I scan the following products with valid sku and quantities")]
        public void WhenIScanTheFollowingProductsWithValidSkuAndQuantities(Table table)
        {
            var products = table.CreateSet<ScanProduct>().AsQueryable();

            products
                .ToList()
                .ForEach(p =>
                _checkoutContext.CheckoutService.ScanProduct(p.Sku, p.Quantity));
        }

        [Given(@"I scan the following products with valid sku and quantities")]
        public void GivenIScanTheFollowingProductsWithValidSkuAndQuantities(Table table)
        {
            var products = table.CreateSet<ScanProduct>().AsQueryable();

            products
                .ToList()
                .ForEach(p =>
                _checkoutContext.CheckoutService.ScanProduct(p.Sku, p.Quantity));
        }

        [Then(@"the following order lines are returned")]
        public void ThenTheFollowingOrderLinesAreReturned(Table table)
        {
            // Arrange
            var expectedOrderLines = table.CreateSet<ScanProduct>()
                .AsQueryable();

            var expectedOrderLinesCount = expectedOrderLines.Select(p => p.Sku)
                .Distinct()
                .Count();

            // Act
            var actualOrderLines = _checkoutContext.CheckoutService.Products
                .GroupBy(x => x.Sku)
                .Select(y => new ScanProduct
                {
                    Id = y.Select(z => z.Id).FirstOrDefault(),
                    Sku = y.Select(z => z.Sku).FirstOrDefault(),
                    Name = y.Select(z => z.Name).FirstOrDefault(),
                    Price = y.Select(z => z.Price).FirstOrDefault(),
                    Quantity = y.Select(z => z.Sku).Count(),
                    Total = y.Select(z => z.Price).FirstOrDefault() * y.Select(z => z.Sku).Count()
                }).AsQueryable();

            // Assert
            actualOrderLines.Should().NotBeNull();
            actualOrderLines.Count().Should().Equals(expectedOrderLinesCount);
            actualOrderLines.ToList().ForEach(o => o.Equals(expectedOrderLines));
        }

        [Then(@"the following order total is returned")]
        public void ThenTheFollowingOrderTotalIsReturned(Table table)
        {
            // Arrange
            var expectedOrderTotal = table.CreateSet<OrderTotal>()
                .AsQueryable();

            // Act
            var actualOrderQuantity = _checkoutContext.CheckoutService.Products.Count;
            var actualOrderTotal = _checkoutContext.CheckoutService.Total;

            // Assert
            actualOrderQuantity.Should().BeGreaterThan(0);
            actualOrderQuantity.Should().Equals(expectedOrderTotal.Select(q => q.Quantity).FirstOrDefault());
            actualOrderTotal.Should().BeGreaterThan(0);
            actualOrderTotal.Should().Equals(expectedOrderTotal.Select(q => q.Total).FirstOrDefault());
        }

        [When(@"I apply already configured discounts ""(.*)""")]
        public void WhenIApplyAlreadyConfiguredDiscounts(string discountScheme)
        {
            _checkoutContext.CheckoutService.ApplyDiscount();
        }

        [Then(@"the following discounted order total is returned")]
        public void ThenTheFollowingDiscountedOrderTotalIsReturned(Table table)
        {
            // Arrange
            var expectedOrderTotal = table.CreateSet<OrderTotal>()
                .AsQueryable();

            // Act
            var actualOrderQuantity = _checkoutContext.CheckoutService.Products.Count;
            var actualOrderTotal = _checkoutContext.CheckoutService.Total;

            // Assert
            actualOrderQuantity.Should().BeGreaterThan(0);
            actualOrderQuantity.Should().Equals(expectedOrderTotal.Select(q => q.Quantity).FirstOrDefault());
            actualOrderTotal.Should().BeGreaterThan(0);
            actualOrderTotal.Should().Equals(expectedOrderTotal.Select(q => q.Total).FirstOrDefault());
        }
    }
}