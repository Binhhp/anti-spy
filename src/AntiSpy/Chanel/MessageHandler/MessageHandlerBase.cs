public abstract class MessageHandlerBase<TMessage>(ILogger _logger, IServiceProvider _serviceProvider) : IMessageHandler where TMessage : class
{
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
    protected abstract Task Handle(TMessage message);
}
