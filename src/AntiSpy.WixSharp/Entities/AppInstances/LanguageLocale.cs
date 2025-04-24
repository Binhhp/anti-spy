using Newtonsoft.Json;

namespace WixSharp.Entities.AppInstances
{
    public class LanguageLocale
    {
        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
