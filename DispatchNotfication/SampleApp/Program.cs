using System;
using System.Threading.Tasks;
using NServiceBus;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "SampleEndpoint";
        var config = new EndpointConfiguration("SampleEndpoint");
        config.UseTransport<LearningTransport>();

        config.NotifyDispatch(new SampleDispatchWatcher());

        var endpoint = await Endpoint.Start(config).ConfigureAwait(false);

        while (Console.ReadKey(true).Key != ConsoleKey.Escape)
        {
            await endpoint.SendLocal(new SomeMessage());
        }

        await endpoint.Stop().ConfigureAwait(false);
    }
}