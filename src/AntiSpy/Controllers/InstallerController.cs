using AntiSpy.Business.BusinessExceptions;
using AntiSpy.Business.BusinessExceptions.Extensions;
using AntiSpy.Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;
public class InstallerController(Serilog.ILogger _logger, UnitOfWork _unitOfWork, AppSetting appSetting) : Controller
{
    [Route("installer/authorize")]
    public IActionResult Authorize(string token, params string[] values)
    {
        _logger.Debug("App authorize for client with token", token);
        try
        {
            token.ThenThrowIfNull(Exceptions.NotFound("Access tokens"));
            string installUrl = $"{appSetting.WixSetting.UriInstall}?token={token}&appId={appSetting.WixSetting.ClientId}&redirectUrl=https://{Request.Host.Value}/installer/install";
            return Redirect(installUrl);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error when install wix app", token, appSetting.WixSetting);
        }
        return Redirect("~/");
    }

    [Route("installer/install")]
    public async Task<IActionResult> Install(string code, string state, string instanceId)
    {
        _logger.Debug("Install App", "code: {@code}", "state {@state}", "instanceId {@instanceId}", code, state, instanceId);
        var redirectUri = await _unitOfWork.Store.InstallAsync(code, instanceId);
        return Redirect(redirectUri);
    }
}