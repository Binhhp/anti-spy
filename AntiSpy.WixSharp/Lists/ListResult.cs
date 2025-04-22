using WixSharp.Filters;
using WixSharp.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace WixSharp.Lists
{
    public class ListResult
    {
        public int totalResults { get; set; }
        public ListResultMetaData metadata { get; set; }
    }
    public class ListResultMetaData
    {
        public int items { get; set; }
        public int offset { get; set; }

    }
}