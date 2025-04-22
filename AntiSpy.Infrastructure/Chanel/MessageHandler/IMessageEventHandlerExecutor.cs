using AntiSpy.Infrastructure.Containers.LifeScoped;

namespace AntiSpy.Infrastructure.Broker.MessageHandler
{
    public interface IMessageEventHandlerExecutor : ISingletonDependency
    {
        Task Execute(object message);
    }
}
