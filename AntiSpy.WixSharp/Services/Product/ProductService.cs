using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using WixSharp.Filters;
using WixSharp.Lists;

namespace WixSharp
{
    /// <summary>
    /// A service for manipulating Wix products.
    /// </summary>
    public class WixProductService : WixService
    {
        /// <summary>
        /// Creates a new instance of <see cref="WixProductService" />.
        /// </summary>
        /// <param name="myWixUrl">The shop's *.myWix.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public WixProductService(string shopAccessToken) : base(shopAccessToken) { }

       
        public virtual async Task<ProductListResult> ListAsync(ProductRootQuery filter = null, CancellationToken cancellationToken = default)
        {
            return await ExecutePostAsync<ProductListResult>(PrepareRequestForStores("products/query"), filter, cancellationToken);
        }
    }
    public class ProductRootQuery
    {
        [JsonProperty("query")]
        public ListFilter Query { get; set; }

        [JsonProperty("includeVariants")]
        public bool IncludeVariants { get; set; }

        [JsonProperty("includeHiddenProducts")]
        public bool IncludeHiddenProducts { get; set; }
    }
}
