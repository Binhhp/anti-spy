using AntiSpy.Infrastructure.Containers.LifeScoped;

public class UnitOfWork : IScopedDependency
{
    public StoreService Store { get; private set; }
    public AntiCopySettingService Settings { get; private set; }
    public UnitOfWork(StoreService storeService, AntiCopySettingService antiCopySettings)
    {
        Store = storeService;
        Settings = antiCopySettings;
    }
}
