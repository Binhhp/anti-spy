using AntiSpy.Infrastructure.Containers.LifeScoped;

public class UnitOfWork : IScopedDependency
{
    public StoreService Store { get; private set; }
    public AntiCopySettingService AntiCopySettings { get; private set; }
    public UnitOfWork(StoreService storeService, AntiCopySettingService antiCopySettings)
    {
        Store = storeService;
        AntiCopySettings = antiCopySettings;
    }
}
