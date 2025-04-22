using AntiSpy.Infrastructure.Containers.LifeScoped;

namespace AntiSpy.Business.Services
{
    public class UnitOfWork : IScopedDependency
    {
        public StoreService Store { get; private set; }
        public UnitOfWork(StoreService storeService)
        {
            Store = storeService;
        }
    }
}
