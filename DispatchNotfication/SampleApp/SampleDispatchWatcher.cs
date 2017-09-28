using System;
using System.Threading.Tasks;
using NServiceBus.Pipeline;
using NServiceBus.Routing;

class SampleDispatchWatcher : IWatchDispatches
{
    public Task Notify(IDispatchContext context)
    {
        foreach (var operation in context.Operations)
        {
            Console.WriteLine($"Dispatched {operation.Message.MessageId} to {Read(operation.AddressTag)}");
        }
        return Task.CompletedTask;
    }

    private string Read(AddressTag addressTag)
    {
        if (addressTag is UnicastAddressTag u)
            return $"Unicast: {u.Destination}";
        if (addressTag is MulticastAddressTag m)
            return $"Multicast: {m.MessageType}";
        throw new ArgumentException(
            message: "addressTag is not a recognized address type",
            paramName: nameof(addressTag));
    }
}