using System.Threading.Tasks;

namespace WixSharp.Services.Script
{
    public class ManagerScriptService : WixService
    {
        public ManagerScriptService(string shopAccessToken) : base(shopAccessToken)
        {
        }

        public async Task EmbedScript(EmbedScriptProperties req)
        {
            await ExecutePostAsync("apps/v1/scripts", new EmbedScriptDto()
            {
                Properties = req
            });
        }

        public async Task<EmbedScriptDto> GetEmbedScript()
        {
            try
            {
                var res = await ExecuteGetAsync<EmbedScriptDto>("apps/v1/scripts");
                return res;
            }
            catch
            {
                return null;
            }
        }
    }
}
