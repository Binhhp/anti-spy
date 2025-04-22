using System.Threading.Channels;
using AntiSpy.Infrastructure.Broker.QueueInMemory;

namespace AntiSpy.Infrastructure.Broker
{
    public partial class ProgramQueue
    {
        public static Channel<object> RequestChannel { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }
        public static List<RequestWorker> RequestWorkers { get; set; }
        public static void InitiateWorkers(WorkerChannelSetting option)
        {
            if(option.NumberOfRequestWorkers > 0)
            {
                RequestWorkers = Enumerable.Range(1, option.NumberOfRequestWorkers)
                    .Select(i => new RequestWorker(i.ToString(), RequestChannel.Reader)).ToList();
            }
        }
    }
}
