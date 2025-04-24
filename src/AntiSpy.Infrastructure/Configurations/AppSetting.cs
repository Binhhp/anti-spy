namespace AntiSpy.Infrastructure.Configurations
{
    public class AppSetting
    {
        public WixSetting WixSetting { get; set; }
    }

    public class WixSetting
    {
        public string UriInstall { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool TestCheckout { get; set; }
        public int CheckoutUrlExpired { get; set; }
        public string RedirectAdmin { get; set; }
    }
}
