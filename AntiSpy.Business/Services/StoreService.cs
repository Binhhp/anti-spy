using AntiSpy.Business.BusinessExceptions;
using AntiSpy.Business.BusinessExceptions.Extensions;
using AntiSpy.Entities.Entities;
using AntiSpy.Infrastructure.Configurations;
using AntiSpy.Infrastructure.Containers.LifeScoped;
using Serilog;
using TiktokWidget.Business.Context;
using WixSharp;
using WixSharp.Services.AppInstances;
using WixSharp.Services.BusinessInfo;

namespace AntiSpy.Business.Services
{
    public class StoreService(AppSetting _appSetting, ILogger _logger, AntiSpyDbContext _dbContext) : IScopedDependency
    {
        public async Task<string> InstallAsync(string code, string instanceId)
        {
            _logger.Debug("End request install wix {@code} {@clientId} {@clientSecret}", code, _appSetting.WixSetting.ClientId, _appSetting.WixSetting.ClientSecret);
            var authentication = new WixAuthorizationService();
            var tokenStore = await authentication.AuthorizeWithResult(code, _appSetting.WixSetting.ClientId, _appSetting.WixSetting.ClientSecret);
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
            if(store == null)
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
                };
                _dbContext.Store.Add(storeEntity);
                _dbContext.SaveChanges();
                return string.Format(_appSetting.WixSetting.RedirectAdmin, _appSetting.WixSetting.ClientId, instanceId, $"/?instanceId={instanceId}");
            }
            else
            {
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
            return string.Format(_appSetting.WixSetting.RedirectAdmin, _appSetting.WixSetting.ClientId, instanceId, string.Empty);
        }
    }
}
