using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AntiSpy.Infrastructure.Broker.MessageHandler
{
    class MessageEventHandlerExecutor : IMessageEventHandlerExecutor
    {
        private IServiceScopeFactory _serviceScopeFactory;
        private ILogger _logger;
        public MessageEventHandlerExecutor(IServiceScopeFactory serviceScopeFactory, ILogger logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public Task Execute(object message)
        {
            var messageType = message.GetType();
            var checkHandlerBase = typeof(MessageHandlerBase<>).MakeGenericType(messageType);
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                try
                {
                    var messageHandlers = scope.ServiceProvider.GetServices<IMessageHandler>();
                    var messageHandler = messageHandlers.LastOrDefault(m =>
                        checkHandlerBase.IsAssignableFrom(m.GetType()));
                    if (messageHandler != null)
                    {
                        return messageHandler.Handle(message);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"Error when handle message executor {JsonConvert.SerializeObject(message)}");
                }
            }
            return Task.CompletedTask;
        }
    }
}
