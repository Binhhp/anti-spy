using Newtonsoft.Json;

namespace WixSharp.Entities.AppInstances
{
    public class AppInstanceSite
    {
        [JsonProperty("siteDisplayName")]
        public string SiteDisplayName { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("paymentCurrency")]
        public string PaymentCurrency { get; set; }

        [JsonProperty("multilingual")]
        public Multilingual Multilingual { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("ownerEmail")]
        public string OwnerEmail { get; set; }

        [JsonProperty("ownerInfo")]
        public OwnerInfo OwnerInfo { get; set; }

        [JsonProperty("siteId")]
        public string SiteId { get; set; }

    }
}
