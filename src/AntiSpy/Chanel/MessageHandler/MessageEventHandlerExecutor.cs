using Newtonsoft.Json;

class MessageEventHandlerExecuto(IServiceScopeFactory _serviceScopeFactory, ILogger _logger) : IMessageEventHandlerExecutor
{
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
