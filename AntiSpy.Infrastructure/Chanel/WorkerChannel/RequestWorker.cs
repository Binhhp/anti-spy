using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;
using AntiSpy.Infrastructure.Broker.MessageHandler;

namespace AntiSpy.Infrastructure.Broker.QueueInMemory
{
    public class RequestWorker
    {
        public string Id { get; }
        private readonly ChannelReader<object> _reader;
        private CancellationTokenSource _cts;
        private Task _task;
        public RequestWorker(string id, ChannelReader<object> reader)
        {
            Id = id;
            _reader = reader;
            StartNew();
        }
        public void StartNew()
        {
            _cts = new CancellationTokenSource();
            _task = Task.Run(async () =>
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    var command = await _reader.ReadAsync(_cts.Token);
                    try
                    {
                        using var scope = ProgramQueue.ServiceProvider.CreateScope();
                        {
                            var _messageEventHandler = scope.ServiceProvider.GetRequiredService<IMessageEventHandlerExecutor>();
                            await _messageEventHandler.Execute(command);
                        }
                    }
                    catch
                    {
                    }
                }
                _cts.Token.ThrowIfCancellationRequested();
            });
        }
    }
}
