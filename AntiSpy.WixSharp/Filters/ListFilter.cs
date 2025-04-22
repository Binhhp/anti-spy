using Newtonsoft.Json;
using System.Collections.Generic;

namespace WixSharp.Filters
{

    public class ListFilter
    {
        [JsonProperty("paging")]
        public Paging Paging { get; }

        [JsonProperty("filter")]
        public string Filter { get; set; }
        
        [JsonProperty("sort")]
        public string Sort { get; set; }
        
        protected ListFilter()
        {
            
        }

        public ListFilter(int limit, int offset = 0, string filter = null, string sort = null)
        {
            Paging = new Paging(limit, offset);
            Filter = filter;
            Sort = sort;
        }

    }

    public class Paging
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }
        [JsonProperty("offset")]
        public int Offset { get; set; }
        public Paging(int limit, int offset)
        {
            Limit = limit;
            Offset = offset;

        }
    }
}
