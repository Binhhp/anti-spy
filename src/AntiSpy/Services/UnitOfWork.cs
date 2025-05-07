using AntiSpy.Infrastructure.Containers.LifeScoped;

public class UnitOfWork : IScopedDependency
{
    public StoreService Store { get; private set; }
    public SettingService Settings { get; private set; }
    public UnitOfWork(StoreService storeService, SettingService antiCopySettings)
    {
        Store = storeService;
        Settings = antiCopySettings;
    }
}
