using System.Threading.Tasks;

namespace WixSharp.Services.Script
{
    public class ManagerScriptService : WixService
    {
        public ManagerScriptService(string shopAccessToken) : base(shopAccessToken)
        {
        }

        public async Task EmbedScript(EmbedScriptDto req)
        {
            await ExecutePostAsync("apps/v1/scripts", req);
        }

        public async Task<EmbedScriptDto> GetEmbedScript()
        {
            var res = await ExecuteGetAsync<EmbedScriptDto>("apps/v1/scripts");
            return res;
        }
    }
}
