using System;
using System.Threading.Tasks;
using NServiceBus.Pipeline;

class DispatchNotificationBehavior : Behavior<IDispatchContext>
{
    public override async Task Invoke(IDispatchContext context, Func<Task> next)
    {
        await next().ConfigureAwait(false);

        foreach (var watch in watches)
        {
            await watch.Notify(context).ConfigureAwait(false);
        }
    }

    private readonly IWatchDispatches[] watches;

    public DispatchNotificationBehavior(IWatchDispatches[] watches)
    {
        this.watches = watches;
    }
}