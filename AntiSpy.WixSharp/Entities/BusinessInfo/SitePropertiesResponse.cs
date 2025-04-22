using Newtonsoft.Json;

namespace WixSharp.Entities.BusinessInfo
{
    public class SitePropertiesResponse
    {
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("properties")]
        public SiteProperties Properties { get; set; }
    }
}
