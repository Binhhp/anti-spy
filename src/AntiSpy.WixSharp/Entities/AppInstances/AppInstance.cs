using Newtonsoft.Json;
using System.Collections.Generic;

namespace WixSharp.Entities.AppInstances
{
    public class AppInstance
    {
        [JsonProperty("instanceId")]
        public string InstanceId { get; set; }

        [JsonProperty("isFree")]
        public bool IsFree { get; set; }

        [JsonProperty("appName")]
        public string AppName { get; set; }

        [JsonProperty("appVersion")]
        public string AppVersion { get; set; }

        [JsonProperty("permissions")]
        public IEnumerable<string> Permissions { get; set; }

        [JsonProperty("billing")]
        public AppBillingInfo Billing { get; set; }
    }
}
