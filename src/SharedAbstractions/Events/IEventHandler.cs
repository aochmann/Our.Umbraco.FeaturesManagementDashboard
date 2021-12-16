using System.Threading.Tasks;
using SharedAbstractions.Domain;

namespace SharedAbstractions.Events
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        ValueTask HandleAsync(TEvent @event);
    }
}