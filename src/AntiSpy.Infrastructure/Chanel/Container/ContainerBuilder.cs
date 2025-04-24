using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;


public static class ChanelContainerBuilder
{
    public static void UseChannels(this IServiceCollection services, Func<WorkerChannelSetting, WorkerChannelSetting>? option = null)
    {
        var serviceProvider = services.BuildServiceProvider();
        ProgramQueue.ServiceProvider = serviceProvider;
        ProgramQueue.RequestChannel = Channel.CreateUnbounded<object>();

        var workerChanelSetting = new WorkerChannelSetting();
        var optionDepend = serviceProvider.GetService(typeof(WorkerChannelSetting));
        if (optionDepend != null)
        {
            workerChanelSetting = (WorkerChannelSetting)optionDepend;
        }
        if (option != null)
        {
            workerChanelSetting = option(workerChanelSetting);
        }
        ProgramQueue.InitiateWorkers(workerChanelSetting);
    }
}
