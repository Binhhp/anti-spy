using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WixSharp.Entities.AppInstances;
using WixSharp.Infrastructure;

namespace WixSharp.Services.AppInstances
{
    public class AppInstanceService : WixService
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public AppInstanceService(string shopAccessToken) : base(shopAccessToken)
        {
        }

        /// <summary>
        /// Retrieves data about the installation of your app on the user's website
        /// </summary>
        public async Task<AppInstanceResponse> GetAsync()
        {
            var req = PrepareRequestForAppInstance($"instance");
            var resp = await ExecuteRequestAsync<AppInstanceResponse>(req, HttpMethod.Get, CancellationToken.None);
            return resp.Result;
        }
        /// <summary>
        /// Close window
        /// </summary>
        public async Task CloseWindow()
        {
            await ExecuteGetAsync($"installer/close-window?access_token={_AccessToken}");
        }
        public async Task SetupInComplete()
        {
            await ExecutePostAsync("apps/v1/bi-event", new
            {
                eventName = "APP_FINISHED_CONFIGURATION"
            });
        }

        /// <summary>
        /// Get Billing checkout
        /// </summary>
        public async Task<GetUrlBillingResponse> GetUrlBillingAsync(CheckoutBillingDto billingDto)
        {
            var req = PrepareRequestForAppInstance("checkout");
            var resp = await ExecuteRequestAsync<GetUrlBillingResponse>(req, HttpMethod.Post, CancellationToken.None, content: new JsonContent(billingDto));
            return resp.Result;
        }
    }
}
