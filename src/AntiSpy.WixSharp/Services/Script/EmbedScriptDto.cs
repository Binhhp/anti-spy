using Newtonsoft.Json;

namespace WixSharp.Services.Script
{
    public class EmbedScriptDto
    {
        [JsonProperty("properties")]
        public EmbedScriptProperties Properties { get; set; }
        public EmbedScriptDto()
        {
        }
        public EmbedScriptDto(EmbedScriptProperties properties)
        {
            Properties = properties;
        }
    }

    public class EmbedScriptProperties
    {
        [JsonProperty("parameters")]
        public Dictionary<string, string> Parameters { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }
    }
}
