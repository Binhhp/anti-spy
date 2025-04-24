using Newtonsoft.Json;
using WixSharp.Entities.AppInstances;

namespace WixSharp.Entities
{
    public class SupportedLanguage
    {
        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }

        [JsonProperty("locale")]
        public LanguageLocale Locale { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("isPrimary")]
        public bool IsPrimary { get; set; }

        [JsonProperty("resolutionMethod")]
        public string ResolutionMethod { get; set; }
    }
}
