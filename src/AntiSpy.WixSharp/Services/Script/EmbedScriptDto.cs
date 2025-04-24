using Newtonsoft.Json;
using System.Collections.Generic;

namespace WixSharp.Services.Script
{
    public class EmbedScriptDto
    {
        [JsonProperty("properties")]
        public EmbedScriptProperties Properties { get; set; }
    }

    public class EmbedScriptProperties
    {
        [JsonProperty("parameters")]
        public Dictionary<string, string> Parameters { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }
    }
}
