using AntiSpy.Infrastructure.Containers.LifeScoped;

public interface IMessageEventHandlerExecutor : ISingletonDependency
{
    Task Execute(object message);
}
