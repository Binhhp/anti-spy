using System.Threading.Tasks;
using WixSharp.Entities.BusinessInfo;

namespace WixSharp.Services.BusinessInfo
{
    public class BusinessService : WixService
    {
        public BusinessService(string shopAccessToken) : base(shopAccessToken)
        {
        }

        /// <summary>
        /// Get site properties
        /// </summary>
        public async Task<SitePropertiesResponse> GetSitePropertiesAsync()
        {
            var siteProperties = await ExecuteGetAsync<SitePropertiesResponse>("site-properties/v4/properties");
            return siteProperties;
        }
    }
}
