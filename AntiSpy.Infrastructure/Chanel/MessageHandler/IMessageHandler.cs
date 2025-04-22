using AntiSpy.Infrastructure.Containers.LifeScoped;
using System.Threading.Tasks;

namespace AntiSpy.Infrastructure.Broker.MessageHandler
{
    public interface IMessageHandler : ISingletonDependency
    {
        /// <summary>
        /// Handle a message.
        /// </summary>
        /// <param name="messageBase">Message.</param>
        /// <returns></returns>
        public Task Handle(object messageBase);
    }
}
