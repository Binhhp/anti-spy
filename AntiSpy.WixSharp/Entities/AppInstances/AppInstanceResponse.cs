using Newtonsoft.Json;

namespace WixSharp.Entities.AppInstances
{
    public class AppInstanceResponse
    {
        [JsonProperty("instance")] 
        public AppInstance Instance { get; set; }

        [JsonProperty("site")] 
        public AppInstanceSite Site { get; set; }
    }
}
