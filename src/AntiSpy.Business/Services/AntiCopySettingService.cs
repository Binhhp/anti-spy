using AntiSpy.Business.BusinessExceptions;
using AntiSpy.Business.BusinessExceptions.Extensions;
using AntiSpy.Infrastructure.Containers.LifeScoped;
using Microsoft.EntityFrameworkCore;

public class AntiCopySettingService(AntiSpyDbContext _context) : IScopedDependency
{
    public StoreEntity Get(string instanceId)
    {
        var store = _context.Store.Include(x => x.AntiCopySettings).FirstOrDefault(x => x.InstanceId == instanceId);
        return store;
    }
    public async Task Set(string instanceId, AntiCopySettingsRequest request)
    {
        var store = _context.Store.FirstOrDefault(x => x.InstanceId == instanceId);
        store.ThenThrowIfNull(Exceptions.NotFound(instanceId));
        store.AntiCopySettings = request.ToEntity();
        await _context.AntiCopySetting.AddAsync(store.AntiCopySettings);
        await _context.SaveChangesAsync();
    }
}
