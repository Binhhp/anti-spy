using Serilog;
using System;
using System.Threading.Tasks;

namespace AntiSpy.Infrastructure.Broker.MessageHandler
{
    public abstract class MessageHandlerBase<TMessage> : IMessageHandler where TMessage : class
    {
        protected ILogger _logger;
        protected IServiceProvider _serviceProvider;
        protected MessageHandlerBase(ILogger logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        async Task IMessageHandler.Handle(object messageBase)
        {
            _logger.Debug("Begin => IMessageHandler.Handle {@messageBase}", messageBase);
            try
            {
                switch (messageBase)
                {
                    case TMessage message:
                        await Handle(message);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error when handle message {@messageBase}", messageBase);
            }
            _logger.Debug("End => IMessageHandler.Handle");
        }
        /// <summary>
        /// Implement to handling message.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <returns></returns>
        protected abstract Task Handle(TMessage message);
    }
}
