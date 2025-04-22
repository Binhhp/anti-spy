using System.Collections.Generic;
using Newtonsoft.Json;

namespace WixSharp.Lists
{
    public class ProductListResult : ListResult
    {
        [JsonProperty("totalResults")] public int TotalResults { get; set; }

        [JsonProperty("metadata")] public MetaData MetaData { get; set; }

        [JsonProperty("products")] public IEnumerable<ProductWix> Products { get; set; }
    }
    public class MetaData
    {
        [JsonProperty("items")]
        public int Items { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}