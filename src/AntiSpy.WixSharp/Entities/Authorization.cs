using Newtonsoft.Json;

namespace WixSharp
{
    /// <summary>
    /// An entity representing a Wix product.
    /// </summary>
    public class WixAuthorization
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
