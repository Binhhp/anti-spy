using AntiSpy.Business.BusinessExceptions;
using AntiSpy.Business.BusinessExceptions.Extensions;
using AntiSpy.Infrastructure.Containers.LifeScoped;
using Microsoft.EntityFrameworkCore;

public class AntiCopySettingService(AntiSpyDbContext _context) : IScopedDependency
{
    public ResponseResult<StoreResponse> GetBySiteId(string siteId)
    {
        try
        {
            var store = _context.Store.Include(x => x.Settings).FirstOrDefault(x => x.SiteId == siteId && !x.IsDeleted);
            store.ThenThrowIfNull(Exceptions.NotFound(siteId));
            return new ResponseResult<StoreResponse>(new StoreResponse(store));
        }
        catch (Exception ex)
        {
            return new ResponseResult<StoreResponse>().WihError("invalid_siteId", ex.Message);
        }
    }
    public ResponseResult<StoreResponse> Get(string instanceId)
    {
        try
        {
            var store = _context.Store.Include(x => x.Settings).FirstOrDefault(x => x.InstanceId == instanceId && !x.IsDeleted);
            store.ThenThrowIfNull(Exceptions.NotFound(instanceId));
            return new ResponseResult<StoreResponse>(new StoreResponse(store));
        }
        catch(Exception ex)
        {
            return new ResponseResult<StoreResponse>().WihError("invalid_instanceId", ex.Message);
        }
    }
    public async Task<ResponseResult<object>> Set(string instanceId, AntiCopySettingsRequest request)
    {
        var result = new ResponseResult<object>();
        try
        {
            var store = _context.Store.Include(x => x.Settings).FirstOrDefault(x => x.InstanceId == instanceId && !x.IsDeleted);
            store.ThenThrowIfNull(Exceptions.NotFound(instanceId));
            var newSettings = request != null ? request.ToEntity() : null;
            if (newSettings == null)
            {
                if (store.Settings != null)
                {
                    _context.Settings.Remove(store.Settings);
                    store.Settings = null;
                }

                await _context.SaveChangesAsync();
                return result;
            }

            if (store.Settings == null)
            {
                if (newSettings == null) return result;
                store.Settings = newSettings;
                _context.Entry(store.Settings).State = EntityState.Added;
            }
            else
            {
                newSettings.Id = store.Settings.Id;
                newSettings.CreatedTime = store.Settings.CreatedTime;
                newSettings.ModifyTime = store.Settings.ModifyTime;
                newSettings.StoreId = store.Settings.StoreId;
                _context.Entry(store.Settings).CurrentValues.SetValues(newSettings);
            }

            await _context.SaveChangesAsync();
            return result;
        }
        catch(Exception ex)
        {
            return result.WihError("invalid_request", ex.Message);
        }
    }
}
