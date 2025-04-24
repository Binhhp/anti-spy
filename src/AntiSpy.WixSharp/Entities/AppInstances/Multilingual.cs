using Newtonsoft.Json;
using System.Collections.Generic;

namespace WixSharp.Entities.AppInstances
{
    public class Multilingual
    {
        [JsonProperty("isMultiLingual")]
        public bool IsMultiLingual { get; set; }

        [JsonProperty("supportedLanguages")]
        public IEnumerable<SupportedLanguage> SupportedLanguages { get; set; }
    }
}
