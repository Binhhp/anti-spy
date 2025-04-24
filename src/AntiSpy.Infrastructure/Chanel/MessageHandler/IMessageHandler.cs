using AntiSpy.Infrastructure.Containers.LifeScoped;
public interface IMessageHandler : ISingletonDependency
{
    public Task Handle(object messageBase);
}
