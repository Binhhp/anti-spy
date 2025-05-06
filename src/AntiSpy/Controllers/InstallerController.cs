using AntiSpy.Business.BusinessExceptions;
using AntiSpy.Business.BusinessExceptions.Extensions;
using AntiSpy.Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;

[ApiExplorerSettings(IgnoreApi = true)]
public class InstallerController(Serilog.ILogger _logger, UnitOfWork _unitOfWork, AppSetting appSetting) : ControllerBase
{
    [Route("installer/authorize")]
    public IActionResult Authorize(string token, params string[] values)
    {
        _logger.Debug("App authorize for client with token", token);
        try
        {
            token.ThenThrowIfNull(Exceptions.NotFound("Access tokens"));
            string installUrl = $"{appSetting.WixSetting.UriInstall}?token={token}&appId={appSetting.WixSetting.AppId}&redirectUrl=https://{Request.Host.Value}/installer/install";
            return Redirect(installUrl);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error when install wix app", token, appSetting.WixSetting);
        }
        return Redirect("~/");
    }

    [Route("installer/install")]
    public async Task<IActionResult> Install(string code, string instanceId, string state = "")
    {
        var redirectUri = await _unitOfWork.Store.InstallAsync(code, instanceId);
        return Redirect(redirectUri);
    }
}