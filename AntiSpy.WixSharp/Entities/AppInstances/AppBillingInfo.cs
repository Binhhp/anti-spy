using Newtonsoft.Json;

namespace WixSharp.Entities.AppInstances
{
    public class AppBillingInfo
    {
        [JsonProperty("packageName")]
        public string PackageName { get; set; }

        [JsonProperty("billingCycle")]
        public string BillingCycle { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }  
    }
}
