using Newtonsoft.Json;

namespace WixSharp.Entities.AppInstances
{
    public class GetUrlBillingResponse
    {
        [JsonProperty("checkoutUrl")]
        public string CheckoutUrl { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
