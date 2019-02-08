namespace FreshCo.Retail.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces;
    using Exceptions;
    using Domain.Entities;

    public sealed class CheckoutService : ICheckoutService
    {
        private readonly IProductService _productService;

        private readonly IDiscountService _discountService;

        private readonly List<Product> _products;

        public CheckoutService(IProductService productService, IDiscountService discountService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
            _products = new List<Product>();
            Total = 0;
        }

        public decimal Total { get; private set; }

        public IReadOnlyList<Product> Products => _products;

        public void ScanProduct(string sku, int quantity)
        {
            if (string.IsNullOrEmpty(sku)) throw new ArgumentNullException(nameof(sku));

            try
            {
                var product = _productService.GetProductBySku(sku);
                _products.AddRange(Enumerable.Repeat(product, quantity));
                Total += product.Price * quantity;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ApplyDiscount()
        {
            if (_products.Any())
            {
                try
                {
                    var discounts = _products
                        .Distinct()
                        .Select(x => _discountService.GetDiscountBySku(x.Sku))
                        .ToList();

                    if (discounts.Any())
                    {
                        foreach (Discount discount in discounts)
                        {
                            var q = _products.Where(p => p.Sku == discount.Sku).Count();
                            if (q >= discount.Quantity)
                            {
                                Total -= discount.Value;
                            }
                        }
                    }
                }
                catch (NotFoundException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}