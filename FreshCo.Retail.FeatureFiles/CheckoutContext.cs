namespace FreshCo.Retail.FeatureFiles
{
    using FreshCo.Retail.Application.Services;

    public class CheckoutContext
    {
        public CheckoutService CheckoutService { get; set; }

        public CheckoutContext() { }
    }
}