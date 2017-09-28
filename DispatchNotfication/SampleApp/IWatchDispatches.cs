using System.Threading.Tasks;
using NServiceBus.Pipeline;

interface IWatchDispatches
{
    Task Notify(IDispatchContext context);
}