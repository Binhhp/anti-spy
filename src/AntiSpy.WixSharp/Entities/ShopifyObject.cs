using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WixSharp
{
    public abstract class WixObject
    {

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
