using AntiSpy.Business.BusinessExceptions;
using AntiSpy.Business.BusinessExceptions.Extensions;
using AntiSpy.Infrastructure.Configurations;
using AntiSpy.Infrastructure.Containers.LifeScoped;
using Microsoft.EntityFrameworkCore;
using WixSharp;
using WixSharp.Services.AppInstances;
using WixSharp.Services.BusinessInfo;
using WixSharp.Services.Script;

public class StoreService(AppSetting _appSetting, ILogger _logger, AntiSpyDbContext _dbContext) : IScopedDependency
{
    public async Task<ResponseResult<EmbedScriptDto>> EmbeddedScripts(string instanceId)
    {
        var store = _dbContext.Store.FirstOrDefault(x => x.InstanceId == instanceId);
        store.ThenThrowIfNull(Exceptions.NotFound(instanceId));

        var authorize = new WixAuthorizationService();
        var accessToken = await authorize.GetAccessToken(_appSetting.WixSetting.AppId, _appSetting.WixSetting.AppSecret, instanceId);
        accessToken.ThenThrowIfNull(Exceptions.NotFound($"Access token from {instanceId}"));
        var managerScript = new ManagerScriptService(accessToken.AccessToken);

        var embeddedScriptInfo = await managerScript.GetEmbedScript();
        if(embeddedScriptInfo == null)
        {
            try
            {
                var property = new EmbedScriptProperties
                {
                    Disabled = false,
                    Parameters = null
                };
                await managerScript.EmbedScript(property);
                embeddedScriptInfo = new EmbedScriptDto(property);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, $"Could not embedded scripts {instanceId}");
            }
        }
        return ResponseResult<EmbedScriptDto>.WithData(embeddedScriptInfo);
    }
    public async Task<ResponseResult<object>> UninstallAsync(string instanceId)
    {
        var store = _dbContext.Store.Include(x => x.Settings).FirstOrDefault(x => x.InstanceId == instanceId && !x.IsDeleted);
        store.ThenThrowIfNull(Exceptions.NotFound(instanceId));
        store.IsDeleted = true;
        _dbContext.Settings.Remove(store.Settings);
        await _dbContext.SaveChangesAsync();
        return ResponseResult<object>.WithSuccess();
    }
    public async Task<string> InstallAsync(string code, string instanceId)
    {
        try
        {
            _logger.Debug("End request install wix {@code} {@clientId} {@clientSecret}", code, _appSetting.WixSetting.AppId, _appSetting.WixSetting.AppSecret);
            var authentication = new WixAuthorizationService();
            var tokenStore = await authentication.AuthorizeWithResult(code, _appSetting.WixSetting.AppId, _appSetting.WixSetting.AppSecret);
            _logger.Debug("End token store", tokenStore);
            tokenStore.ThenThrowIf(r => tokenStore == null, Exceptions.NotFound(code));

            var appInstanceService = new AppInstanceService(tokenStore.AccessToken);
            var appInstanceResp = await appInstanceService.GetAsync();
            _logger.Debug("Get app instance {@appInstanceResp}", appInstanceResp);

            var businessService = new BusinessService(tokenStore.AccessToken);
            var shopRequest = await businessService.GetSitePropertiesAsync();
            _logger.Debug("End get site properties {@shopRequest}", shopRequest);

            shopRequest.ThenThrowIf(s => s == null || s.Properties == null, Exceptions.NotFound(instanceId));

            var store = _dbContext.Store.FirstOrDefault(x => x.InstanceId == instanceId);
            if (store == null)
            {
                var storeEntity = new StoreEntity
                {
                    InstanceId = instanceId,
                    AppInstanceName = appInstanceResp.Instance.AppName,
                    Country = shopRequest.Properties?.Address?.Country,
                    Currency = shopRequest.Properties?.PaymentCurrency,
                    Email = shopRequest.Properties?.Email,
                    Token = tokenStore.AccessToken,
                    RefreshToken = tokenStore.RefreshToken,
                    Phone = shopRequest.Properties?.Phone,
                    BusinessName = shopRequest.Properties?.BusinessName,
                    Timezone = shopRequest.Properties?.TimeZone,
                    SiteDescription = appInstanceResp.Site.Description,
                    SiteDisplayName = appInstanceResp.Site.SiteDisplayName,
                    SiteId = appInstanceResp.Site.SiteId,
                    SiteUrl = appInstanceResp.Site.Url,
                    IsDeleted = false
                };
                _dbContext.Store.Add(storeEntity);
                _dbContext.SaveChanges();
            }
            else
            {
                store.IsDeleted = false;
                store.Token = tokenStore.AccessToken;
                store.RefreshToken = tokenStore.RefreshToken;
                store.Timezone = shopRequest.Properties.TimeZone;
                _dbContext.SaveChanges();
            }

            try
            {
                var appInstance = new AppInstanceService(tokenStore.AccessToken);
                await appInstance.SetupInComplete();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error when setup complete wix store " + store.AppInstanceName);
            }
        }
        catch(Exception ex)
        {
            _logger.Error(ex, "Could not install store " + instanceId);
        }
        return string.Format(_appSetting.WixSetting.RedirectAdmin, _appSetting.WixSetting.AppId, instanceId, string.Empty);
    }
}
