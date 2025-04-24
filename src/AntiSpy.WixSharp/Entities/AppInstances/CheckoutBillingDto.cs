using Newtonsoft.Json;

namespace WixSharp.Entities.AppInstances
{
    public class CheckoutBillingDto
    {
        [JsonProperty("productId")]
        public string ProductId { get; set; }
        
        [JsonProperty("successUrl")]
        public string SuccessUrl { get; set; }
        
        [JsonProperty("testCheckout")]
        public bool TestCheckout { get; set; }
        
        [JsonProperty("billingCycle")]
        public string BillingCycle { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }

        [JsonProperty("couponCode")]
        public string CouponCode { get; set; }
    }
}
