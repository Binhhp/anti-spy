using Newtonsoft.Json;

namespace WixSharp.Entities.AppInstances
{
    public class OwnerInfo
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("emailStatus")]
        public string EmailStatus { get; set; }
    }
}
